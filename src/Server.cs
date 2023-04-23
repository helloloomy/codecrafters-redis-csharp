using System.Net;
using System.Net.Sockets;
using System.Text;

TcpListener server = new TcpListener(IPAddress.Any, 6379);

server.Start();
var handler = await server.AcceptSocketAsync();

while (handler.Connected)
{
    try
    {
        var buffer = new byte[handler.Available];
        var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
        var receivedMsg = Encoding.UTF8.GetString(buffer, 0, received);
        Console.WriteLine($"Received message: {receivedMsg}");

        var msg = "+PONG\r\n";
        var msgBytes = Encoding.UTF8.GetBytes(msg);
        await handler.SendAsync(msgBytes, SocketFlags.None);

        Console.WriteLine("Sent PONG response!");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
    
}