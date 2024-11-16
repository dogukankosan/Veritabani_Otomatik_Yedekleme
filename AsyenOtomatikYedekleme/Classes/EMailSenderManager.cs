using AsyenYedekleme.Classes;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyenOtomatikYedekleme.Classes
{
    internal class EMailSenderManager
    {
        internal async static Task MailSendForm(string senderEmail, string recipientEmail, string senderPassword, string server, int port, bool ssl)
        {
            string subject = "Asyen Yedekleme Programından Test E-posta Başlığı";
            string body = "Bu bir test e-posta içeriğidir.";
            try
            {
                using (SmtpClient client = new SmtpClient(server, port))
                {
                    client.EnableSsl = ssl;
                    client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    MailMessage mail = new MailMessage
                    {
                        From = new MailAddress(senderEmail),
                        Subject = subject,
                        Body = body
                    };
                    mail.To.Add(recipientEmail);
                    await client.SendMailAsync(mail);
                    XtraMessageBox.Show("E-Mail Başarıyla Gönderildi.", "Başarılı Mail Gönderme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("E-Mail gönderilirken bir hata oluştu: " + ex.Message, "Hatalı E-Posta Gönderimi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextLog.TextLogging(ex.ToString());
            }
        }
        internal async static Task MailSend()
        {
            DataTable dt = SQLLiteConnection.GetDataFromSQLite("SELECT EMail,RecipientEmail,Password,Server,Port,SSL FROM EMailSetting LIMIT 1");
            if (dt.Rows.Count == 0)
            {
                TextLog.TextLogging("Mail Bilgileri Girilmemiştir Mail Gönderilemedi");
                return;
            }
            if (string.IsNullOrEmpty(dt.Rows[0][1].ToString()))
            {
                TextLog.TextLogging("Mail Alıcı Bilgisi Girilmemiştir Mail Gönderilemedi");
                return;
            }
            string subject = "Yedekleme İşlemi Başarılı";
            string body = "Veritabanlarınız Başarılı Bir Şekilde Yedeklendi";
            try
            {
                using (SmtpClient client = new SmtpClient(dt.Rows[0][3].ToString(), int.Parse(dt.Rows[0][4].ToString())))
                {
                    client.EnableSsl = dt.Rows[0][5].ToString() == "1" ? true : false;
                    client.Credentials = new NetworkCredential(dt.Rows[0][0].ToString(), EncryptionHelper.Decrypt(dt.Rows[0][2].ToString()));
                    MailMessage mail = new MailMessage
                    {
                        From = new MailAddress(dt.Rows[0][0].ToString()),
                        Subject = subject,
                        Body = $@"
<html>
    <body style='font-family: Arial, sans-serif;'>
        <div style='text-align: center; margin-bottom: 20px;'>
            <h1 style='color: #4CAF50; margin-bottom: 10px;'>Merhaba!</h1>
            <p style='color: #333; font-size: 16px;'>
               {body}
            </p>
        </div>
        <div style='position: relative; margin-top: 40px;'>
            <div style='text-align: left; font-size: 12px; color: #333;'>
                <a href='http://www.asyen.com.tr' target='_blank'>
                    <img  src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAPoAAABlCAMAAABTAg6ZAAABR1BMVEVHcEzm7/Ycebk9i8Itg75ko87W5vHI3OkWdbeAs9fB1+gRc7YTdLZHkcVGi8E0h8Aqerg3icEfe7qVutROlsgUdLYXdrcvgr0lfbtUgriSudljos5EkcUZd7g2aKgTdLZQmMlemsgbd7cogL06isImfboifbsrgr09isFSmMkWdrcWdbctgb0fe7oyhsAdebklfbsrf7wogLyQttAsgr0jZaklWJ7yrGxBkMZuq9QogLxCfLYacrMlbK4feLgycK9eos8he7oXdLYmX6Qofbswi8YvaKk5dLHzr3I8jsUsiMT0s3glYaUiW6H1uoT2wZEgVp30tn70uYMPcrUcWKAeX6UdW6IcVp4eYacfZKkmh8QfZqsgaa0idbYjebkdXaMPcbUlgcAkfLwhb7EgbK8hcrQRc7UbUZobU5wdXqQlhMIkf74TdLa8yRCnAAAAU3RSTlMAAdsYuhMEDu0PCeXxH0AtWGvUIzP3+XSMKQc5Y+GE/FoK6JBMe77FWSYc/ZXJr4aynaQbhvLe/okq0ErM7POlRqvU2+fds2janOqp6fBwN/NTiEhVjEYAAA8eSURBVHja7Jv5V9pKG4AJlAgIYZGAjbIKCBVaNr/iUqnWanu0K1X06nVpNb21/v8/f7NlMktUwJ5zz4E77bElJJBnnvedmcyMLtd/5b9yXzG8E4u+llYmlDy387+nkyr9888J1Z7b+flzQrUD6T8nUzuUPqHakfSJ1J7b+efnhGpf+4zRJ087kP7PhGpf+9C32CdMe26n3+9PpnYgvf8Y7UoljIvH6V01pGWj0awWZh+PVHTJU9U+8BQeCJEv94SlYp2qWgfoxR7ho4aS/q1vs4+gPRQL4lL3y+Bawx1MLC0lgr7FaoHSq22fzxdMafQ8LQWu9xXJq2LQRz7S+s/WM/LWsy14JSjuODkS9YGTap7RpCN0i3147eVED5d8XHzL2/b1rHLRC9aS1h3G8/BQw3IVaMKXqTB5+aInlmmKPk2OmG1yo8lb8Mo9CrrRPfxmsY+k3Wha93ebFN7yNzI8gTlLYNUqV1eaj4UZCL3nDj0Wfe0DQP/2CO2VFPhqfdsEP0tCxCcF8t4t9Yy1V1Wm9qj0wdAz0UeiA+mHPPuw2qOQzz0fBD99caERQLcYqyaTnZI7wYU4px1Ln1U49ODiE7s0PSx6ZluHFe19HLq2f4jYmXQfTntgEyG1alLEKwVYHZlGCOAquVZh1p3pNQIuVruJtBsNJL3i4tBXQzmmKCz6UgdWql5+FHqge35ua++PoL0CETJl16wc8UV4aJoSKaFo7T1FV6umpV3IdIK+6DhZiNGjbXhx03gMuraP0PmQH0o7/uaQCyn2zcnoYTYF5uz+F2tfUFxqg8/0QdDRxTi/RkQ3uge/ZPZhtAfqJIU9MAbNIvvea1gbZl0LOF6Jtafidh0Mga420TXq6Oja8sEvyk7T/Z8htKP7hlmnLPTEiPfWUFPsqxc1j3LHpeZCoEGqQEBfXWnR4hfRXQXdah5GQwfSD6D289G1W/EORja6FPFlMqAxg7VmNB6QtKPuOZoXMp2g67FVWqISurcEm5jkyOhAOmYXQ35g7f4S7bJwxLc5uPkU7ZkzvnrZ76A9s2EKmU7Qzd4FLS8kdNyn1rwjohvdoyNn9oG1x6HXBOplcMTXuAxVtHrQHpfomxX+uWfBpKM8RUJniwN6yE0ybSR07dURYichzwxoB812BTXiZEiJ0i+oCWFRqMYSFKEWlrX3JOkSupN1BfVvmwFXdGl4dKN7fMyxD6/di+N9Sg0EAioa0fIRj6rHU0jWU8RvySs38pJ0kutb61bZ2pXRcb35tJHQtVfHDDsf8gNq11A4b22iUvfJEW/VciVagjFBckPULkiXR3OGAzoe+FdHQTe6JxRdYh9Q+6z0nNELFui7M2xq+5OomqqKrJ3r0wfr12mCuSsjoGuvILpjyPcH1E46br7M0rezq0nm9vEz+fMplzTqSbx2jYKOepfb5HxiWHQg/eROdoA+iPZC0AHdjvhsIlOaD1HDGJ3v3edgkmyMhk76t87Q6EA6Rr8r3QfQjuI9GKPFneEiPpuAj6wLhRDE9UZ9zBP6AOirM1NMURzQUf+WWM0MiW40Tk957eKA9mHtaBDTa87Q8eacm4v4LOrVTD1WB41gTWfnFwZADy6W7LLpcUB3of4NdRLDoGuvTkV2Md0f1I7iXWfabKXBRXx2w+qXQSHZ0BoYHc7mXZB/hVkaC52OC4ZBN7qnGP2edH9IOxmDh9hBu85GvD8aM/l2wC0MeO5Hv3OCykK3pwWHQEfSqfZjxx6u/4D2kBvKbKjSIbuvqhRjzPScXpembOd84AIZ/UIoHPrFhYUO+jdywuDoRuPqimV3TvcHtL9ejbndsQIXCG03KM+ZtrlVbq5vJ3qmueGWHl9AWVkF56/OiY+DbrGULDRPCXzpataKqyZ5/4lncOlXjux8yPfT9/fqM7AY3KDV6Zh/ZS0SiWgtw2mmA50fcPxkrlixpaBXtA5b4vuDSOfRnXu4N//2CpxCyoMnDZ7p+R8i+9H6upzu/bFbeFUbP35I7Ltry1K6//va/3QUxfMEnWFf9+R2HHr3MdNuNK5/iNpPwBPx2rKc7uO13g6kI3SOfR0MTPw7Dr37WGlXq9fXVDthP0UrRlj7AZfuY5Xtc3mCbrOfbKHRKNAup3t6nKT/fc2yI+1k0QRol9jHSHt8G6Fz7FtkJimw4zCYT4+R9L9F9iu6UmZpZ9N9bLQD6Sw7QrdXtqF2KeTHpJFH0gk71c7MgkPtIvuYaMfS+ZBnFzmnoHZxZJMeD+lnZwL71RWdKfz09evH7LL87D4O2pX49pnIfpW3Jo1a716+3Jvqys/u39LjIR2gn3HpTmeYPr58+e4TyHaZfUjt/rVIXB3IRKQw4G/XhSKRyiMz/exM1O7T0FRJwDUDpH9V+UVIEvLDaa/U9V6+aDwsIpnv6fWwVCGq3KNotUyGnwobVnrn+5nE3jRcU3vv3n1UvyLpLka7zS5oV7UIW+LcnJuKVs19Be7csOOECZxFb7B15NfmO+8bnajwkXiBK2bN/easLw473ZNmOEr/LrJD6YoCQ/0jlA7rO+ew9vyN79v9T9iJ4kyqFGXiFi9MZKwl8cBz+HLB4XaKePHgmT1RVy7le6bZA3/ytTaDsIIWboLWxG1ouofnees5djKY3M6i31G6hH69GSANHCjvZkjfLrPz2nl0CMrsl8B7TOjuwbvR0RIps0TXqursZ/o2Nau+K2h3KN2nE7I2i+aZ/C/r96BD6SL7Bm7evyD0jySeug4z8+l70Xu3ddv7PLyJWuVB9FANaNfpSpS/IaxX2AuOxoJ+cWE2/SK6Pm+3ENXe3ehK5/d3ib2J54Gn9qD0Fh3SyTPznHaMniEFe7f3iBrR+uImHSbdje4KN0qlKA3rKPkgPZMxxTVbb7FUWqDLPBSdWf/AaXYHevwtRmfZN0hjpH7a29v7Qu+96zAzn5bQN7OokP0S7G6KALOIfg86qHJ7Bt6DmrJgKVooZNubMV1YZp5iPjIkbQwnm03vQFc7v39L2h1+b8HSLrKz2hE62RmpKN4Gvxc2VACFNlJ3oxsaPNEKNbQXK7OAb0gNFZp5pjafwjPDHLqe5xa0YZupb2cc1+WBdIl9g66SBr58+WLXK9IupnvaGR2OTHwc+vxGIrG06uHRHZ7+PDFdT2xkyavXG/yOQyO+YC9gtRPg1DaHnn+vM5uWUNuaajqiK53L3xL7EyodjGdQp26Fz7K8EMdo59HxrUzT2EM7PGIDoKOdARY6WnMNsruM1AptB2a5dXv0fb5sikkzVPv19q0T+srby0uKTtht6YqAbmlnQ/7Q7tsFdNSvbOZGQqfW8dazVLLiNCJxQn/dxJvHaBtpFpNO6Grn8lLSzpzUAs0cu+qPNpYJA1pbO0Z/YaCNXf6Cm/c1mnWyh85M1ZOaVx0AfQ6CkvpHC+2+uaITOpQusm8wvaJnbmpqLqQK2vl0P0xz6Jlpsp8PtU/VwCPRQTdPBzNbzWhFfRAd7anADXUIBz9G9wuZfnMpsVvVo3jX0jtlRSnvdCOUXtB+zmmXR3MN5vtGRHeFS/Y2BDNVjT+EnivRAR1KuQW8XZZHV1be3tjsJN2/R8kTQ/rNhw+74NaUyP7nN4DewNrldXeqXUI3S5ryaHSXNxmzh7Kmu2zcj446NLQvCe1M1suO6J2bG0n7ogdx7+wf9hE5KJH9/uGvN93dpzlLO5/ulnYymlvKLC1lyNgrX1YejQ6gys2Ybo1nfVHlfnS0TRX+AgwayoHhjQM6lC6xR/1acX35ADBZ5JD98PD84OjNzm7c22W22Vg9XJpFr8/j0t5C+1y1P4COfgU2Wc9jenvS0BkdEadCZKtuU3XJLbza+etGYt+urr+CXL/O93ft+4rsn/86ODo6PnlVr5/KIU+0W52boirwbwU1UE3jT6CjS+Jt/ATavh/dinO0f25p3uWAjqXf8Ol+fQrRANfyLntbkX1Ifnxyenp16rDNBvftQr+OvzIVug/d9RB66//tnN9P2zAQx+0BY83aqnRAqhIY6ygLG7/aUalIC2IbCIVXNGlAG9Jfe6D7/593Z8eJ7bi0ZU/Rdk+lJG4++Tr2+XyXHbmjbpSVpZsZXYxuLJKB3mQKnbbv7h512QPccUJ2lRzZOXlnODSk2XDZdXQGGzuhz0Nf3fwqx1eOlGy5CegsPFFe4q5czoB+einQJdlHDP3+VientOTd3qPow0HHkFm2ZUDnhWp/h44LgVozUf69kgg4AZ15Mvki823iCiAJHUVPsQcYgAf21lU6dFryOPlgaMgsY7JH3lxuidker9SO1y8m9OZpYp/NqjMffre+U1iAJus1JY16AjqvF94/FuV+ujeHoqvsgN5lmw8dz0AO87vHyAcjU1bdVjzCv/7GLSruiANlJvT1zdhqoq4rpTqbzs8OTk52WYo1V/JJdHTowot1sY7RVOei36mPe787QnTPNofLSx6ij0aiy8uPO8qeDlDJS2cTulzDs29CJ3tRmGXNXrZWuGdzUJiGzi4kjP+poUeiq10+6GIIPm+bg+Xg03pI3u0O04mkD1tG9HxyF6eg35jR6WozL4a2j6w8rlwk09B5WjjeMHbjtQ4vRJfZxzwkaVsTQ/YuZp3AQYYu/32B5D6oybs3awdu0n+cT/CVhK7n+kroYSjN69ViY/MtW4ZC5//1qiGFWyv4KzJ6GHL0Yj5kKec8UlX/DR8T9O3rNHs/AKoz+4kNElqqAXnAZFfZf1TekVx7Ubbmvitl59LzQ/jOFxdgqceCCf90yYc/Ds+V4MRKY/HQdY9fLFaUnQQHz4sXmgtNPPGUtdHgbfI+5+LHdjxL0BR7b4wRmqc0x9PcGqAHIy1v/OFnBVqmllybsawNGFWlYkM7Fkz8LjWdTHL4mpbCsqHJ+HqlE0XrlnRYMltL7JHsY1yst60p21QO26DTZG8d5TK1waqx92DZNp0cus9F0O93lVKJVt0iJMPs6MG3Z0FwcLtmKHX5ll0lWbNteYYDj6Y9U7elzgU4P0neeMu2CMkye2/c82d8YKlzPR4PBHvrqkqyaOcxe+/Rn3moAvZeEFVGGZ3eLGTRUMH+OAc5Pu/XkezeVXbT5wS7P9fLoUH3ABOIvVKWEwc5uz/na7GBHWTPNjkhG5fzk7NxPvPkhG5c+s94FTp1vpRI5u3Ns14C/7Lwr74//b9ly/4AU/8CFjgcEQUAAAAASUVORK5CYII=' alt='Asyen Bilişim Logo' style='width: 50px; height: 50px; float: left; margin-right: 10px;' />
                    <br/>      <br/>
                </a>
                <p>
                    <strong>ASYEN BİLİŞİM TEKNOLOJİLERİ SANAYİ VE TİCARET LTD.ŞTİ.</strong><br />
                    Geçmişten Bugüne. Bugünden Geleceğe.<br />
                    CUMHURİYET MAH. D-100 KARAYOLU CAD. ADM KONAKLAMA SİTESİ<br />
                    OUTLET PARK AVM BLOK NO:371 İÇKAPI NO:63<br />
                    BÜYÜKÇEKMECE / İSTANBUL / TÜRKİYE
                </p>
            </div>
        </div>
        <footer style='margin-top: 20px; text-align: left;'>
            <p style='color: #999;'>Bu bir otomatik e-posta'dır, lütfen yanıt vermeyin.</p>
        </footer>
    </body>
</html>",
                        IsBodyHtml = true
                    };
                    mail.To.Add(dt.Rows[0][1].ToString());
                    await client.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                TextLog.TextLogging(ex.ToString());
                XtraMessageBox.Show(ex.Message, "Hatalı Mail Gönderme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}