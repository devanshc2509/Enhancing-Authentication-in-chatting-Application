using System;
using System.Collections.Generic; using System.ComponentModel; using System.Data; using System.Drawing; using System.Linq; using System.Text; using System.Threading.Tasks; using
System.Windows.Forms; using AesEncDec;
namespace chatting_app
{
public partial class AES : Form
{
public AES()
{
InitializeComponent();
}
private void BtnEncrypt_Click(object sender, EventArgs e)
{ string plaintext = txtplainforencrypt.Text; txtencrypt.Text = AesCryp.Encrypt(plaintext);
}
private void BtnDecrypt_Click(object sender, EventArgs e)
{
string encrypted = txtencrypt.Text; txtdecryptedplain.Text = AesCryp.Decrypt(encrypted);
}
private void btnClear_Click(object sender, EventArgs e)
{ txtdecryptedplain.Text = ""; txtencrypt.Text = ""; txtplainforencrypt.Text = "";
}
private void btnback_Click(object sender, EventArgs e)
{
Form1.staticVar2.Show();
}
private void btnCopyencryptedtext_Click(object sender, EventArgs e) { Clipboard.SetText(txtencrypt.Text);
MessageBox.Show("Encrypted Text Copied !");
} private void btnCopyDecryptedtext_Click(object sender, EventArgs e) { Clipboard.SetText(txtdecryptedplain.Text);
MessageBox.Show("Decrypted Text Copied !");
}
} }