using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmVille_api.src.Database.Objects
{
    public class MessagePrinter
    {
        // Enum to represent message types
        public enum MessageType {
            SUCCESS,
            ERROR
        }

        // Method to print messages
        public static void printMessage(MessageType messageType, String message) {
            switch (messageType) {
                case MessageType.SUCCESS:
                    System.Console.WriteLine(MessageType.SUCCESS + "\n");
                    break;
                case MessageType.ERROR:
                    throw new Exception(message + "\n" + MessageType.ERROR + "\n");
                default:
                    throw new Exception("Unsupported message type: " + messageType);
            }
        }
    }
}