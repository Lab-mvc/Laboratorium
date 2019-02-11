using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models{
  public class ChatModel{
      // lista uzytkownnkow
        public List<ChatUser> Users;

        // Wiadomosci
        public List<ChatMessage> Messages;
          
        public ChatModel(){
            Users = new List<ChatUser>();
            Messages = new List<ChatMessage>();
        }
  }
  public class ChatUser{
      public string Name;
        public DateTime LoginTime;
        public DateTime LastPing;
  }
  public class ChatMessage{
      // autor
        public ChatUser User;
        // czas
        public DateTime Date = DateTime.Now;
        // tekst
        public string Text = "";
  }
}
