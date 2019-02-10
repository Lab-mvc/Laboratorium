using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        static ChatModel chatModel;
        public ActionResult Index(string user, bool? logOn, bool? logOff, string chatMessage)
        {
            try
            {
                if (chatModel == null) chatModel = new ChatModel();

                //wyswietla sie tylko ostatnie 80 
                if (chatModel.Messages.Count > 100)
                    chatModel.Messages.RemoveRange(0, 80);
                if (!Request.IsAjaxRequest())
                {
                    return View(chatModel);
                }
                else if (logOn != null && (bool)logOn)
                {
                    //sprawdzenie loginu
                    if (chatModel.Users.FirstOrDefault(u => u.Name == user) != null)
                    {
                        throw new Exception("Uzytkownk juz istnieje");
                    }
                    //ograniczenie na ilosc uzytkownikow
                    else if (chatModel.Users.Count > 2)
                    {
                        throw new Exception("Czat jest pelny");
                    }
                    else
                    {
                        // dodanie nowego uzytkownika
                        chatModel.Users.Add(new ChatUser()
                        {
                            Name = user,
                            LoginTime = DateTime.Now,
                            LastPing = DateTime.Now
                        });

                        // komunikat o nowym uzytkowniku
                        chatModel.Messages.Add(new ChatMessage()
                        {
                            Text = user + " zalogowal sie",
                            Date = DateTime.Now
                        });
                    }

                    return PartialView("ChatRoom", chatModel);
                }
                else if (logOff != null && (bool)logOff)
                {
                    LogOff(chatModel.Users.FirstOrDefault(u => u.Name == user));
                    return PartialView("ChatRoom", chatModel);
                }
                else
                {
                    ChatUser currentUser = chatModel.Users.FirstOrDefault(u => u.Name == user);

                    //kazdy uzytkownik dostaje swoj ostatni czas aktualizacji
                    currentUser.LastPing = DateTime.Now;

                    // usuniecie nieaktywnych
                    List<ChatUser> removeThese = new List<ChatUser>();
                    foreach (Models.ChatUser usr in chatModel.Users)
                    {
                        TimeSpan span = DateTime.Now - usr.LastPing;
                        if (span.TotalSeconds > 15)
                            removeThese.Add(usr);
                    }
                    foreach (ChatUser u in removeThese)
                    {
                        LogOff(u);
                    }
                    if (!string.IsNullOrEmpty(chatMessage))
                    {
                        chatModel.Messages.Add(new ChatMessage()
                        {
                            User = currentUser,
                            Text = chatMessage,
                            Date = DateTime.Now
                        });
                    }
                    return PartialView("History", chatModel);
                }
            }
            catch (Exception ex)
            {
                //w razie bledu
                Response.StatusCode = 500;
                return Content(ex.Message);
            }
        }

        // przy wyjsciu usuwamy z listy
        public void LogOff(ChatUser user)
        {
            chatModel.Users.Remove(user);
            chatModel.Messages.Add(new ChatMessage()
            {
                Text = user.Name + " oposcil czat",
                Date = DateTime.Now
            });
        }
    }
}