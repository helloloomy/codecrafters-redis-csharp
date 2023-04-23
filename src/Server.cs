using System.Net;
using System.Net.Sockets;
using System.Text;

TcpListener server = new TcpListener(IPAddress.Any, 6379);

try
{
    server.Start();
    using var handler = await server.AcceptTcpClientAsync();
    await using var stream = handler.GetStream();

    var msg = "+PONG\r\n";
    var msgBytes = Encoding.UTF8.GetBytes(msg);
    await stream.WriteAsync(msgBytes);

    Console.WriteLine("Sent PONG response!");
}
finally
{
    server.Stop();
}