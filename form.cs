using System;
using System.Collections.Generic; using System.ComponentModel; using System.Data; using System.Drawing; using System.Linq; using System.Text; using

System.Threading.Tasks; using System.Windows.Forms;
using System.Net; using System.Net.Sockets;
using System.Net.NetworkInformation;
namespace chatting_app
{
public partial class Form1 : Form
{ public static Form1 staticVar = null; public static Form1 staticVar2 = null;
Socket sck;
EndPoint eplocal, epremote; public Form1()
{
InitializeComponent();
sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); sck.SetSocketOption(SocketOptionLevel.Socket,
SocketOptionName.ReuseAddress, true);
txtperson1_ip.Text = GetLocalIP(); txtperson2_ip.Text = GetLocalIP(); txtMac1.Text = GetMACAddress().ToString(); txtMac2.Text = GetMACAddress().ToString();
} private string GetLocalIP()
{
IPHostEntry host;
host = Dns.GetHostEntry(Dns.GetHostName());
foreach(IPAddress ip in host.AddressList)
{
if(ip.AddressFamily == AddressFamily.InterNetwork)
{
return ip.ToString();
}
}
return "127.0.0.1";
}
private static string GetMACAddress()
{ foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
{
if (nic.OperationalStatus == OperationalStatus.Up) return AddressBytesToString(nic.GetPhysicalAddress().GetAddressBytes());
}
return string.Empty;
}
private static string AddressBytesToString(byte[] addressBytes)
{
return string.Join(":", (from b in addressBytes
select b.ToString("X2")).ToArray());
}
private void btnStart_Click(object sender, EventArgs e)
{ try
{
eplocal = new IPEndPoint(IPAddress.Parse(txtperson1_ip.Text),
Convert.ToInt32(txtperson1_port.Text)); sck.Bind(eplocal); epremote = new IPEndPoint(IPAddress.Parse(txtperson2_ip.Text),Convert.ToInt32(txtperson2_port.Text)); sck.Connect(epremote);
byte[] buffer = new byte[1500];
sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epremote, new AsyncCallback(MessageCallBack), buffer);
btnStart.Text = "connected";
btnStart.Enabled = false; btnSend.Enabled = true; message_send.Focus();
}
catch(Exception exe)
{
MessageBox.Show(exe.ToString());
}
}
private void btnSend_Click(object sender, EventArgs e)
{ try
{
System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding(); byte[] msg = new byte[1500]; msg = enc.GetBytes(message_send.Text);
sck.Send(msg);
messagelist.Items.Add("Person 2: " + message_send.Text); message_send.Clear();
}
catch(Exception exe)
{
MessageBox.Show(exe.ToString());
}
}
private void linklblencryptdecrypt_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
{ staticVar2 = this; this.Hide(); AES aes = new AES(); aes.ShowDialog();
}
private void linklblcrypto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
{ staticVar = this; this.Hide();
Hash_code_Generator gen = new Hash_code_Generator(); gen.ShowDialog();
}
private void btncopyIpMac1_Click(object sender, EventArgs e)
{
Clipboard.SetText(txtperson1_ip.Text + "," + txtMac1.Text);
}
private void btncopyIpMac2_Click(object sender, EventArgs e)
{
Clipboard.SetText(txtperson2_ip.Text + "," + txtMac2.Text);
}
private void Form1_KeyDown(object sender, KeyEventArgs e)
{
if (e.KeyCode == Keys.Enter)
{
btnSend.PerformClick();
}
}
private void MessageCallBack(IAsyncResult aResult)

{ try
{
int size = sck.EndReceiveFrom(aResult, ref epremote); if(size > 0)
{
byte[] receivedData = new byte[1464]; receivedData = (byte[])aResult.AsyncState;
ASCIIEncoding eEncoding = new ASCIIEncoding();
string receivedMessage = eEncoding.GetString(receivedData); messagelist.Items.Add("Person1 : " + receivedMessage);
}
byte[] buffer = new byte[1500];
sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epremote, new AsyncCallback(MessageCallBack), buffer);
}
catch(Exception exp)
{
MessageBox.Show(exp.ToString());
}
}
} }