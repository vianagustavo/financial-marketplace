using FinancialMarketplace.Application.Contracts.Tools;

using Microsoft.Extensions.Configuration;

namespace FinancialMarketplace.Infrastructure.Tools;

public class EmailTemplateBuilder(IConfiguration configuration) : IEmailTemplateBuilder
{
    private readonly string _url = configuration["BOM_CONSORCIO_URL"] ?? throw new ArgumentNullException("BOM_CONSORCIO_URL");
    public string DefinePasswordTemplate(string token, string username)
    {
        return $@"<!DOCTYPE html>
                <html lang=""en"">
                  <head>
                    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">
                    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <meta name=""description"" content=""Cuba admin is super flexible, powerful, clean &amp; modern responsive bootstrap 5 admin template with unlimited possibilities."">
                    <meta name=""keywords"" content=""admin template, Cuba admin template, dashboard template, flat admin template, responsive admin template, web app"">
                    <meta name=""author"" content=""pixelstrap"">
                    <link rel=""icon"" href=""/assets/images/favicon.png"" type=""image/x-icon"">
                    <link rel=""shortcut icon"" href=""/assets/images/favicon.png"" type=""image/x-icon"">
                    <title>Cuba - Premium Admin Template</title>
                    <link href=""https://fonts.googleapis.com/css?family=Work+Sans:100,200,300,400,500,600,700,800,900"" rel=""stylesheet"">
                    <link href=""https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"" rel=""stylesheet"">
                    <link href=""https://fonts.googleapis.com/css?family=Poppins:100,100i,200,200i,300,300i,400,400i,500,500i,600,600i,700,700i,800,800i,900,900i"" rel=""stylesheet"">
                    <style type=""text/css"">
                      .body{{
                        color: #000;
                        font-family: work-Sans, sans-serif;
                        background-color: #f6f7fb;
                        display: block;
                      }}
                      img {{
                        margin-bottom: 30px;
                      }}
                      a{{
                        text-decoration: none;
                      }}
                      span {{
                        font-size: 14px;
                      }}
                      .card {{
                        margin: 0 auto; 
                        background-color: #fff; 
                        border-radius: 8px;
                        padding: 30px
                      }}
                      p {{
                        font-size: 13px;
                        line-height: 1.7;
                        letter-spacing: 0.7px;
                        margin-top: 0;
                      }}
                      h6 {{
                        font-size: 16px;
                        margin: 0 0 18px 0;
                        font-weight: 600;
                      }}
                    </style>
                  </head>
                  <body>
                    <div class=""body"">
                        <div style=""margin: auto; width: 650px; padding: 30px;"">
                            <img src=""https://bomconsorcio-dev.s3.amazonaws.com/7338eeec-32f5-41f5-ac7d-289a80b11b52bc-logo.png?AWSAccessKeyId=AKIATGK2N52ZXJMMEHI7&Expires=2022528539&Signature=JjERWNyX2FEY0T3KnQwFY6C783I%3D"" alt=""brand"">
                            <div class=""card"">
                            <h6>Bem Vindo</h6>
                            <p>Olá {username}, foi criada uma conta para você no sistema de gestão de carteira.</p>
                            <p>Para começar a usar o sistema clique abaixo e crie uma senha.</p>
                            <p style=""text-align: center"">
                                <a href=""{_url}/password?token={token}"" style=""padding: 10px; background-color: #7366ff; color: #fff; display: inline-block; border-radius: 4px"">Criar Senha</a>
                            </p>
                            <p style=""margin-bottom: 0"">
                                Atenciosamente,<br>Equipe BomConsórcio
                            </p>
                            </div>
                            <p style=""text-align: center; color: #999; margin-top: 30px;"">Av Tancredo neves 1283 Sl 502 , Salvador - BA</p>
                        </div>
                    </div>
                  </body>
                </html>";
    }

    public string ResetPasswordTemplate(string token, string username)
    {
        return $@"<!DOCTYPE html>
                <html lang=""en"">
                  <head>
                    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">
                    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <meta name=""description"" content=""Cuba admin is super flexible, powerful, clean &amp; modern responsive bootstrap 5 admin template with unlimited possibilities."">
                    <meta name=""keywords"" content=""admin template, Cuba admin template, dashboard template, flat admin template, responsive admin template, web app"">
                    <meta name=""author"" content=""pixelstrap"">
                    <link rel=""icon"" href=""/assets/images/favicon.png"" type=""image/x-icon"">
                    <link rel=""shortcut icon"" href=""/assets/images/favicon.png"" type=""image/x-icon"">
                    <title>Cuba - Premium Admin Template</title>
                    <link href=""https://fonts.googleapis.com/css?family=Work+Sans:100,200,300,400,500,600,700,800,900"" rel=""stylesheet"">
                    <link href=""https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"" rel=""stylesheet"">
                    <link href=""https://fonts.googleapis.com/css?family=Poppins:100,100i,200,200i,300,300i,400,400i,500,500i,600,600i,700,700i,800,800i,900,900i"" rel=""stylesheet"">
                    <style type=""text/css"">
                      .body{{
                        color: #000;
                        font-family: work-Sans, sans-serif;
                        background-color: #f6f7fb;
                        display: block;
                      }}
                      img {{
                        margin-bottom: 30px;
                      }}
                      a{{
                        text-decoration: none;
                      }}
                      span {{
                        font-size: 14px;
                      }}
                      .card {{
                        margin: 0 auto; 
                        background-color: #fff; 
                        border-radius: 8px;
                        padding: 30px
                      }}
                      p {{
                        font-size: 13px;
                        line-height: 1.7;
                        letter-spacing: 0.7px;
                        margin-top: 0;
                      }}
                      h6 {{
                        font-size: 16px;
                        margin: 0 0 18px 0;
                        font-weight: 600;
                      }}
                    </style>
                  </head>
                  <body>
                    <div class=""body"">
                        <div style=""margin: auto; width: 650px; padding: 30px;"">
                            <img src=""https://bomconsorcio-dev.s3.amazonaws.com/7338eeec-32f5-41f5-ac7d-289a80b11b52bc-logo.png?AWSAccessKeyId=AKIATGK2N52ZXJMMEHI7&Expires=2022528539&Signature=JjERWNyX2FEY0T3KnQwFY6C783I%3D"" alt=""brand"">
                            <div class=""card"">
                            <h6>Bem Vindo</h6>
                            <p>Olá {username}, </p>
                            <p>Esqueceu a senha ?</p>
                            <p>Não tem problema, clique no botão abaixo e crie uma nova senha. </p>
                            <p style=""text-align: center"">
                                <a href=""{_url}/reset-password?token={token}"" style=""padding: 10px; background-color: #7366ff; color: #fff; display: inline-block; border-radius: 4px"">Criar Nova Senha</a>
                            </p>
                            <p style=""margin-bottom: 0"">
                                Atenciosamente,<br>Equipe BomConsórcio
                            </p>
                            </div>
                            <p style=""text-align: center; color: #999; margin-top: 30px;"">Av Tancredo neves 1283 Sl 502 , Salvador - BA</p>
                        </div>
                    </div>
                  </body>
                </html>";
    }
}