using System;
using System.Collections.Generic; using System.ComponentModel; using System.Data; using System.Drawing; using System.Linq; using System.Text; using System.Threading.Tasks; using System.Windows.Forms; using System.Security.Cryptography;
namespace chatting_app
{
public partial class Hash_code_Generator : Form
{
public Hash_code_Generator()
{
InitializeComponent();
}
public static string GenerateSHA256String(string inputString)
{
SHA256 sha256 = SHA256Managed.Create(); byte[] bytes = Encoding.UTF8.GetBytes(inputString); byte[] hash = sha256.ComputeHash(bytes); return GetStringFromHash(hash);
}
public static string GenerateSHA512String(string inputString) { SHA512 sha512 = SHA512Managed.Create(); byte[] bytes =
Encoding.UTF8.GetBytes(inputString); byte[] hash = sha512.ComputeHash(bytes); return GetStringFromHash(hash);
}
private static string GetStringFromHash(byte[] hash)
{
StringBuilder result = new StringBuilder(); for (int i = 0; i < hash.Length; i++)
{
result.Append(hash[i].ToString("X2"));
}
return result.ToString();
} private void BtnGenerate_Click(object sender, EventArgs e)
{
string str = txtString.Text;
if (cbxusing.SelectedIndex == 0)
{
txthash.Text = GenerateSHA512String(str);
}
else if (cbxusing.SelectedIndex == 1)
{
txthash.Text = GenerateSHA256String(str);
} else
{
MessageBox.Show("Invalid selection... Please choose Correct option");
}
}
private void BtnReset_Click(object sender, EventArgs e)
{ txtString.Text = ""; txthash.Text = "";
}
private void BtnCopy_Click(object sender, EventArgs e)
{
Clipboard.SetText(txthash.Text); MessageBox.Show("Hash code Copied");
}
private void Btnmatch_Click(object sender, EventArgs e)
{
if (txthash.Text == txtreceivedhash.Text)
{
MessageBox.Show("Your Connection is Secure..\n No spoofing attack is found", "Connection status", MessageBoxButtons.OK, MessageBoxIcon.Information);
} else
{
MessageBox.Show("Your Connection is not Secure..\n someone is spoofing as Receiver", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
}
}
private void btnback_Click(object sender, EventArgs e)
{
Form1.staticVar.Show();
}
}
}