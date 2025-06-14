using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace digital_signature
{
    public class CertificateLoader
    {
        public X509Certificate LoadCertificateFromToken(string pkcs11Path, string pin)
        {
            var factories = new Pkcs11InteropFactories();

            using (IPkcs11Library pkcs11 = factories.Pkcs11LibraryFactory.LoadPkcs11Library(
                    factories, pkcs11Path, AppType.SingleThreaded))
            {
                ISlot slot = pkcs11.GetSlotList(SlotsType.WithTokenPresent)[0];

                using (Net.Pkcs11Interop.HighLevelAPI.ISession session = slot.OpenSession(SessionType.ReadWrite))
                {
                    session.Login(CKU.CKU_USER, pin);

                    List<IObjectAttribute> searchTemplate = new List<IObjectAttribute>
            {
                session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE)
            };

                    List<IObjectHandle> certObjects = session.FindAllObjects(searchTemplate);
                    if (certObjects == null || certObjects.Count == 0)
                        throw new Exception("Không tìm thấy chứng thư số trong USB Token.");

                    var attr = session.GetAttributeValue(certObjects[0], new List<CKA> { CKA.CKA_VALUE });
                    byte[] certBytes = attr[0].GetValueAsByteArray();

                    session.Logout();

                    var cert = new X509CertificateParser().ReadCertificate(certBytes);
                    return cert;
                }
            }
        }
    }
}
