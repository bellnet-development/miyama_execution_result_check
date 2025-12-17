using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using miyama_execution_result_check.BforestDbEntities;
using NLog;
using System.Net.Mail;

namespace miyama_execution_result_check
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ログ出力の競合対策
            var factory = new LogFactory();
            var logger = factory.GetLogger("Log");

            if (args.Count() == 0)
            {
                logger.Error("システムID[引数]が未設定のため処理を終了します。");
                return;
            }

            #region 変数
            string runTimeId = args[0];

            // 引数のプログラム名でディレクトリを作成しログ出力
            logger.Factory.Configuration.Variables.Add("runtime", runTimeId);
            factory.ReconfigExistingLoggers();

            #endregion

            try
            {
                logger.Info("---------- START PROCESSING ----------");
                logger.Info("runTimeId = [" + runTimeId + "]");

                #region 初期化
                if (!File.Exists("./appconfig.json"))
                {
                    logger.Error("appconfig.jsonが配置されていないため処理を終了します。");
                    return;
                }
                IConfigurationRoot? configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory() + @"\")
                                    .AddJsonFile("./appconfig.json")
                                    .Build();
                #endregion

                #region DB接続

                // BFORESTDB初期化
                var contextOptionsForestDb = new DbContextOptionsBuilder<BforestDbContext>()
                        .UseSqlServer(configuration["BforestDbContext"])
                        .Options;
                BforestDbContext bforestDbContext = new(contextOptionsForestDb);

                #endregion

                #region 制御テーブル取得

                var normalResult = bforestDbContext.MONITORING_M_CONTROLs.Where(x => x.SYSTEM_ID == runTimeId && x.END_DIV == "NORMAL")
                                    .AsNoTracking().ToList();

                var errorResult = bforestDbContext.MONITORING_M_CONTROLs.Where(x => x.SYSTEM_ID == runTimeId && x.END_DIV == "ERROR")
                                    .AsNoTracking().ToList();

                if (normalResult.Count() == 0 || errorResult.Count() == 0)
                {
                    logger.Info("正常終了 or 異常終了のレコードが未登録です。");
                    return;
                }

                string normalFileName = string.Empty;
                string errorFileName = string.Empty;

                foreach (var item in normalResult)
                {
                    normalFileName = item.MONITORING_PATH + @"\" + item.MONITORING_FILE;
                }

                foreach (var item in errorResult)
                {
                    errorFileName = item.MONITORING_PATH + @"\" + item.MONITORING_FILE;
                }
                #endregion

                #region ファイル存在チェック
                List<SendMail> sendMailList = new();

                if (!File.Exists(normalFileName) && !File.Exists(errorFileName))
                {
                    // 未実施
                    SendMail mail = new()
                    {
                        SMTP = configuration["MailServerAddress"],
                        PORT = configuration["MailServerPort"],
                        ID = configuration["MailId"],
                        PASSWORD = configuration["MailPassword"],
                        From = configuration["MailFrom"],
                        FromName = configuration["miyama_system"],

                        Subject = errorResult[0].MAIL_SUBJECT,
                        To = errorResult[0].MAIL_TO,
                        Cc = errorResult[0].MAIL_CC,

                        Body = errorResult[0].MAIL_BODY + Environment.NewLine + Environment.NewLine + "正常終了、異常終了ファイルが存在しません。",
                    };
                    sendMailList.Add(mail);
                }
                else
                {
                    bool isNormalEnd = false;

                    if (File.Exists(normalFileName))
                    {
                        // 正常終了
                        logger.Error("正常終了ファイルの存在を確認！");
                        isNormalEnd = true;

                        if (normalResult[0].MAIL_TO != null && normalResult[0].MAIL_TO.ToString().Length != 0)
                        {
                            // 異常終了
                            SendMail mail = new()
                            {
                                SMTP = configuration["MailServerAddress"],
                                PORT = configuration["MailServerPort"],
                                ID = configuration["MailId"],
                                PASSWORD = configuration["MailPassword"],
                                From = configuration["MailFrom"],
                                FromName = configuration["miyama_system"],

                                Subject = normalResult[0].MAIL_SUBJECT,
                                To = normalResult[0].MAIL_TO,
                                Cc = normalResult[0].MAIL_CC,

                                Body = normalResult[0].MAIL_BODY,
                            };

                            sendMailList.Add(mail);
                            SendMail(logger, sendMailList);
                            sendMailList.Clear();
                        }
                    }

                    if (!isNormalEnd)
                    {
                        if (File.Exists(errorFileName))
                        {
                            // 異常終了
                            SendMail mail = new()
                            {
                                SMTP = configuration["MailServerAddress"],
                                PORT = configuration["MailServerPort"],
                                ID = configuration["MailId"],
                                PASSWORD = configuration["MailPassword"],
                                From = configuration["MailFrom"],
                                FromName = configuration["miyama_system"],

                                Subject = errorResult[0].MAIL_SUBJECT,
                                To = errorResult[0].MAIL_TO,
                                Cc = errorResult[0].MAIL_CC,

                                Body = errorResult[0].MAIL_BODY,
                            };

                            mail.Attachment.Add(errorFileName);
                            mail.ContentType.Add("text/plain");
                            sendMailList.Add(mail);
                        }
                    }
                }

                #endregion

                #region メール送信

                if (sendMailList.Count != 0)
                {
# if !DEBUG
                    SendMail(logger, sendMailList);
#endif
                }

                #endregion

                #region ファイル移動

                logger.Info("normalFileName = [" + normalFileName + "]");
                logger.Info("errorFileName = [" + errorFileName + "]");


                DateTime now = DateTime.Now;
                string formattedDateTime = now.ToString("yyyyMMddHHmmss");
                if (File.Exists(normalFileName))
                {
                    string? normalBackUp = normalResult[0].BACK_PATH;
                    string bkDir = normalBackUp + @"\" + now.Year.ToString("0000") + @"\" + now.Month.ToString("00") + @"\" + now.Day.ToString("00");

                    if (!Directory.Exists(bkDir))
                    {
                        Directory.CreateDirectory(bkDir);
                    }

                    logger.Info("normalMove = [" + normalBackUp + @"\" + now.Year.ToString("0000") + @"\" + now.Month.ToString("00") + @"\" + now.Day.ToString("00") + @"\"
                        + runTimeId + "_" + formattedDateTime + "_" + normalResult[0].MONITORING_FILE + "]");

#if DEBUG
                    File.Copy(normalFileName
                        , normalBackUp + @"\" + now.Year.ToString("0000") + @"\" + now.Month.ToString("00") + @"\" + now.Day.ToString("00") 
                        + @"\" + runTimeId + "_" + formattedDateTime + "_" + normalResult[0].MONITORING_FILE, true);
#else
                    File.Move(normalFileName
                        , normalBackUp + @"\" + now.Year.ToString("0000") + @"\" + now.Month.ToString("00") + @"\" + now.Day.ToString("00") + @"\" 
                        + runTimeId + "_" + formattedDateTime + "_" + normalResult[0].MONITORING_FILE, true);
#endif
                }

                if (File.Exists(errorFileName))
                {
                    string? errorBackUp = errorResult[0].BACK_PATH;
                    string bkDir = errorBackUp + @"\" + now.Year.ToString("0000") + @"\" + now.Month.ToString("00") + @"\" + now.Day.ToString("00");

                    if (!Directory.Exists(bkDir))
                    {
                        Directory.CreateDirectory(bkDir);
                    }

                    logger.Info("errorMove = [" + errorBackUp + @"\" + now.Year.ToString("0000") + @"\" + now.Month.ToString("00") + @"\" + now.Day.ToString("00") + @"\"
                        + runTimeId + "_" + formattedDateTime + "_" + errorResult[0].MONITORING_FILE + "]");

#if DEBUG
                    File.Copy(errorFileName
                        , errorBackUp + @"\" + now.Year.ToString("0000") + @"\" + now.Month.ToString("00") + @"\" + now.Day.ToString("00") 
                        + @"\" + runTimeId + "_" + formattedDateTime + "_" + errorResult[0].MONITORING_FILE, true);
#else
                    File.Move(errorFileName
                        , errorBackUp + @"\" + now.Year.ToString("0000") + @"\" + now.Month.ToString("00") + @"\" + now.Day.ToString("00") + @"\" 
                        + runTimeId + "_" + formattedDateTime + "_" + errorResult[0].MONITORING_FILE, true);
#endif

                }
                #endregion
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                logger.Error("ex = [" + ex.Message + "]");
            }
            finally
            {
                logger.Info("---------- END PROCESSING ----------");
            }
        }

        #region メール送信
        private static void SendMail(Logger? logger, List<SendMail> sendMailList)
        {
            // メール送信
            foreach (SendMail send in sendMailList)
            {
                MailMessage mailMessage = new();
                SmtpClient smtpClient = new();

                try
                {
                    // 送信者アドレス、送信者名を設定
                    mailMessage.From = new MailAddress(send.From);
                    mailMessage.IsBodyHtml = false;

                    string[] mailTo = send.To.Split(';');

                    foreach (string to in mailTo)
                    {
                        if (to.Trim().Length != 0)
                        {
                            mailMessage.To.Add(to);
                        }
                    }

                    if(send.Cc != null)
                    {
                        string[] mailCc = send.Cc.Split(';');

                        foreach (string cc in mailCc)
                        {
                            if (cc.Trim().Length != 0)
                            {
                                mailMessage.CC.Add(cc);
                            }
                        }
                    }

                    if(send.Bcc != null)
                    {
                        string[] mailBcc = send.Bcc.Split(';');

                        foreach (string bcc in mailBcc)
                        {
                            if (bcc.Trim().Length != 0)
                            {
                                mailMessage.Bcc.Add(bcc);
                            }
                        }
                    }

                    mailMessage.Subject = send.Subject;
                    mailMessage.Priority = MailPriority.High;
                    mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                    mailMessage.Body = send.Body;

                    // SMTPサーバ
                    smtpClient.Host = send.SMTP;
                    smtpClient.Port = int.Parse(send.PORT);
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Credentials = new System.Net.NetworkCredential(send.ID, send.PASSWORD);
                    smtpClient.EnableSsl = true;

                    int i = 0;
                    foreach (var item in send.Attachment)
                    {
                        Attachment attachment;
                        attachment = new Attachment(item);
                        attachment.ContentType = new System.Net.Mime.ContentType(send.ContentType[i]);
                        mailMessage.Attachments.Add(attachment);
                        i++;
                    }

                    logger.Info("++++++++++++++++++++++++++++++++++++++++++++++++++");
                    logger.Info("メール送信者：[" + mailMessage.From + "]");

                    foreach (MailAddress addTo in mailMessage.To)
                    {
                        logger.Info("メール送信先：[" + addTo.Address + "]");
                    }

                    foreach (MailAddress addCc in mailMessage.CC)
                    {
                        logger.Info("メール送信先CC：[" + addCc.Address + "]");
                    }

                    foreach (MailAddress addBcc in mailMessage.Bcc)
                    {
                        logger.Info("メール送信先BCC：[" + addBcc.Address + "]");
                    }
                    logger.Info("メールタイトル：[" + mailMessage.Subject + "]");
                    logger.Info("メール本文：[" + mailMessage.Body + "]");

                    foreach (var file in mailMessage.Attachments)
                    {
                        logger.Info("添付ファイル名：[" + file.Name + "]");
                    }
                    logger.Info("++++++++++++++++++++++++++++++++++++++++++++++++++");

#if DEBUG
                    smtpClient.Send(mailMessage);
#else
                    smtpClient.Send(mailMessage);
#endif
                }
                catch (Exception ex)
                {
                    logger.Error("ex = [" + ex.Message + "]");
                }
                finally
                {
                    mailMessage.Dispose();
                    smtpClient.Dispose();
                }
            }
        }
        #endregion

    }
}
