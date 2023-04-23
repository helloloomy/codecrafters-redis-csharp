using System.Net;
using System.Net.Sockets;
using System.Text;

TcpListener server = new TcpListener(IPAddress.Any, 6379);

server.Start();
var handler = await server.AcceptTcpClientAsync();

while (handler.Connected)
{
    try
    {
        var stream = handler.GetStream();

        var msg = "+PONG\r\n";
        var msgBytes = Encoding.UTF8.GetBytes(msg);
        await stream.WriteAsync(msgBytes);

        Console.WriteLine("Sent PONG response!");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
    
}