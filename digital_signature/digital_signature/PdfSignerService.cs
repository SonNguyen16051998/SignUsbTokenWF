using iText.Bouncycastle.X509;
using iText.Commons.Bouncycastle.Cert;
using iText.Forms.Fields;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Signatures;
using iText.StyledXmlParser.Jsoup.Parser;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.HighLevelAPI.Factories;
using Net.Pkcs11Interop.HighLevelAPI.MechanismParams;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static iText.Kernel.Font.PdfFontFactory;

namespace digital_signature
{
    public class PdfSignerService
    {
        public void SignPdf(string inputPdfPath, string outputPdfPath, string pkcs11LibPath, string pin, float X, float Y, string folderErrPath, string folderSuccessPath, string certPath, string rootCertpath, string fileNameOutput, string logoPath)
        {
            try
            {
                var factories = new Pkcs11InteropFactories();

                using (var pkcs11 = factories.Pkcs11LibraryFactory.LoadPkcs11Library(factories, pkcs11LibPath, AppType.SingleThreaded))
                {
                    var slot = pkcs11.GetSlotList(SlotsType.WithTokenPresent).FirstOrDefault();
                    if (slot == null) throw new Exception("No token found");

                    using (var session = slot.OpenSession(SessionType.ReadWrite))
                    {
                        session.Login(CKU.CKU_USER, pin);

                        var privKeySearch = new List<IObjectAttribute>
                    {
                        session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_PRIVATE_KEY)
                    };
                        var privKeyObjects = session.FindAllObjects(privKeySearch);
                        if (privKeyObjects.Count == 0)
                            throw new Exception("No private key found on token");
                        var privateKeyHandle = privKeyObjects[0];

                        var certSearch = new List<IObjectAttribute>
                    {
                        session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE)
                    };
                        var certObjects = session.FindAllObjects(certSearch);
                        var certAttr = session.GetAttributeValue(certObjects[0], new List<CKA> { CKA.CKA_VALUE });
                        var certBytes = certAttr[0].GetValueAsByteArray();
                        var userCert = new X509CertificateParser().ReadCertificate(certBytes);

                        //

                        using (var reader = new PdfReader(inputPdfPath))
                        using (var outputStream = new FileStream(outputPdfPath, FileMode.Create))
                        {
                            var signer = new PdfSigner(reader, outputStream, new StampingProperties());

                            float sigWidth = 100;
                            float sigHeight = 50;

                            float x = X;
                            float y = Y;

                            string imgPath = System.IO.Path.Combine(logoPath);
                            ImageData img = ImageDataFactory.Create(imgPath);

                            PdfFont unicodeFont = PdfFontFactory.CreateFont("tahoma.ttf", PdfEncodings.IDENTITY_H, EmbeddingStrategy.PREFER_EMBEDDED);

                            string layer2Text =
                                $"Ký bởi: SỞ NÔNG NGHIỆP VÀ MÔI TRƯỜNG\n" +
                                $"Cơ quan: ỦY BAN NHÂN DÂN THÀNH PHỐ ĐÀ NẴNG\n" +
                                $"Ngày ký: {DateTime.Now:dd-MM-yyyy HH:mm:ss} +07:00";

                            // Appearance
                            PdfSignatureAppearance appearance = signer.GetSignatureAppearance()
                                .SetPageRect(new iText.Kernel.Geom.Rectangle(x, y, sigWidth, sigHeight))
                                .SetPageNumber(1)
                                .SetSignatureGraphic(img)
                                .SetReason("SỞ NÔNG NGHIỆP VÀ MÔI TRƯỜNG <stnmt@danang.gov.vn> đã ký lên văn bản này!")
                                .SetLocation("ỦY BAN NHÂN DÂN THÀNH PHỐ ĐÀ NẴNG")
                                .SetContact("CỤC CHỨNG THỰC SỐ VÀ BẢO MẬT THÔNG TIN - BCYCP - ĐT: 0243.7738668; Fax: 0243.7738668; Email: ca@bcy.gov.vn")
                                .SetLayer2Text(layer2Text)
                                .SetLayer2Font(unicodeFont)
                                .SetReuseAppearance(false)
                                .SetRenderingMode(PdfSignatureAppearance.RenderingMode.GRAPHIC_AND_DESCRIPTION);

                            signer.SetFieldName("Signature1");

                            string crt = System.IO.Path.Combine(certPath);
                            string rootCert = System.IO.Path.Combine(rootCertpath);
                            var parser = new X509CertificateParser();
                            var intermediateCert = parser.ReadCertificate(File.ReadAllBytes(crt));
                            var inrootCert = parser.ReadCertificate(File.ReadAllBytes(rootCert));

                            var pks = new Pkcs11Signature(session, privateKeyHandle, DigestAlgorithms.SHA256);

                            IX509Certificate[] certificateChain = new IX509Certificate[] {
                        new X509CertificateBC(userCert),
                        new X509CertificateBC(intermediateCert),
                        new X509CertificateBC(inrootCert)};
                            IOcspClient ocspClient = new OcspClientBouncyCastle();
                            ICrlClient crlClient = new CrlClientOnline();
                            IExternalDigest externalDigest = new BouncyCastleDigest();

                            signer.SignDetached(
                                externalDigest,
                                pks,
                                certificateChain,
                                new List<ICrlClient> { crlClient },
                                ocspClient,
                                null,
                                0,
                                PdfSigner.CryptoStandard.CADES
                            );
                        }
                        session.Logout();
                    }
                    string timestamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                    string logContent =
                        $"Ký thành công: {fileNameOutput}{Environment.NewLine}" +
                        $"Thời gian ký: {timestamp}{Environment.NewLine}" +
                        $"------------------------------------------------------{Environment.NewLine}";
                    Helpers.WriteLog(folderSuccessPath, logContent);
                }
            }
            catch(Exception e)
            {
                string timestamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                string logContent =
                    $"Ký thất bại: {fileNameOutput}{Environment.NewLine}" +
                    $"Thời gian ký: {timestamp}{Environment.NewLine}" +
                    $"Lỗi: {e.Message}" +
                    $"------------------------------------------------------{Environment.NewLine}";
                Helpers.WriteLog(folderErrPath, logContent);
            }
        }
    }

    public class Pkcs11Signature : IExternalSignature
    {
        private readonly Net.Pkcs11Interop.HighLevelAPI.ISession _session;
        private readonly IObjectHandle _privateKey;
        private readonly string _digestAlgorithm;

        public Pkcs11Signature(Net.Pkcs11Interop.HighLevelAPI.ISession session, IObjectHandle privateKey, string digestAlgorithm)
        {
            _session = session;
            _privateKey = privateKey;
            _digestAlgorithm = digestAlgorithm;
        }

        public string GetDigestAlgorithmName() => _digestAlgorithm;

        public string GetSignatureAlgorithmName() => "RSA";

        public ISignatureMechanismParams GetSignatureMechanismParameters() => null;

        public byte[] Sign(byte[] messageDigest)
        {
            byte[] prefix = GetDigestInfoPrefix(_digestAlgorithm);
            byte[] digestInfo = new byte[prefix.Length + messageDigest.Length];

            Buffer.BlockCopy(prefix, 0, digestInfo, 0, prefix.Length);
            Buffer.BlockCopy(messageDigest, 0, digestInfo, prefix.Length, messageDigest.Length);

            var mechanism = _session.Factories.MechanismFactory.Create(CKM.CKM_RSA_PKCS);
            return _session.Sign(mechanism, _privateKey, digestInfo);
        }

        public byte[] GetDigestInfoPrefix(string digestAlgorithm)
        {
            return digestAlgorithm switch
            {
                DigestAlgorithms.SHA256 => new byte[] {
            0x30, 0x31,
            0x30, 0x0d,
            0x06, 0x09,
            0x60, 0x86, 0x48, 0x01, 0x65, 0x03, 0x04, 0x02, 0x01,
            0x05, 0x00,
            0x04, 0x20
        },
                DigestAlgorithms.SHA1 => new byte[] {
            0x30, 0x21,
            0x30, 0x09,
            0x06, 0x05,
            0x2b, 0x0e, 0x03, 0x02, 0x1a,
            0x05, 0x00,
            0x04, 0x14
        },
                DigestAlgorithms.SHA512 => new byte[] {
            0x30, 0x51,
            0x30, 0x0d,
            0x06, 0x09,
            0x60, 0x86, 0x48, 0x01, 0x65, 0x03, 0x04, 0x02, 0x03,
            0x05, 0x00,
            0x04, 0x40
        },
                _ => throw new NotSupportedException($"Digest algorithm not supported: {digestAlgorithm}")
            };
        }
    }
}
