using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miyama_execution_result_check
{
    public class SendMail
    {
        // SMTPサーバ
        public string? SMTP = string.Empty;
        // PORT番号
        public string? PORT = string.Empty;
        // メールサーバID
        public string? ID = string.Empty;
        // メールサーバパスワード
        public string? PASSWORD = string.Empty;
        // メール送信者
        public string? From = string.Empty;
        // メール送信者 氏名
        public string? FromName = string.Empty;
        // メールタイトル
        public string? Subject = string.Empty;
        // TOアドレス
        public string? To = string.Empty;
        // CCアドレス
        public string? Cc = string.Empty;
        // BCCアドレス
        public string? Bcc = string.Empty;
        // メール本文
        public string? Body = string.Empty;
        // 添付ファイル
        public List<string> Attachment = new();
        // 添付ファイルタイプ
        public List<string> ContentType = new();
    }
}
