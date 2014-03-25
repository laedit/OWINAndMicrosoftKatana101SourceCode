using(var provider = new System.Security.Cryptography.RNGCryptoServiceProvider())
{
  byte[] secretKeyBytes = new Byte[32];
  provider.GetBytes(secretKeyBytes);
  
  var key = Convert.ToBase64String(secretKeyBytes);
}
