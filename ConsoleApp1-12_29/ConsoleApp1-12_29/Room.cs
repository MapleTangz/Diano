using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    class Room
    {
        public int id = -1;
        public int hostid;
        public string hostname;
        public bool hostready;
        public int guestid;
        public string guestname;
        public bool guestready;
        public bool readytostart;
        public int hostscore;
        public int guestscore;
        public bool hostfinish;
        public bool guestfinish;
        public bool hostdead;
        public bool guestdead;

        public Room(int roomid)
        {
            id = roomid;
            hostid = -1;
            hostname = "";
            hostready = false;
            guestid = -1;
            guestname = "";
            guestready = false;
            readytostart = false;
            hostscore = 0;
            guestscore = 0;
            hostfinish = false;
            guestfinish = false;
        }

        public int RoomID()
        {
            return id;
        }

        public int GetHostID()
        {
            return hostid;
        }

        public string GetHostName()
        {
            return hostname;
        }

        public int GetGuestID()
        {
            return guestid;
        }

        public string GetGuestName()
        {
            return guestname;
        }

    }
}
