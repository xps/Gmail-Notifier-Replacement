What is it?
===============================

This is an open-source alternative to the official Google Gmail Notifier (now retired).

![Screenshot][3]

Supposedly Google retired the app as it was replaced by browser notifications, but these only show when your browser in open.

**This app will notify you of new emails even when your browser is closed.**


Features
===============================

This is rather simple and aims to work just like the original Google Notifier.

It will show the mail icon in the system tray, turning blue when you have
unread mail. A balloon tooltip will pop up when you receive mail. Hovering over the
icon will show a tooltip letting you know how many unread emails you have.

Double-clicking the icon will open your inbox in the your default browser.

It will check mail every couple of minutes via [Gmail's Atom Feed][1].

**Note:** This feed is only available for Gmail accounts on Google Apps domains (...@gmail.com).


Installation
===============================

Download the portable binaries or the installer [here][2], or install with [Chocolatey][6]:

    C:\> choco install gmailnotifier

The installer and the Chocolatey package will set up the app to start automatically with Windows. If you download the binaries, you will need to set that up yourself, if you want it.


Getting started
===============================

First, you will need to **generate an [application-specific password][4]** in your Google Account. It will not work with your real password (and you shouldn't be giving your account password to any app anyway).

On the first run, you will be asked for credentials (these will be stored [securely][5]).

**Requires the .NET Framework 3.5 or above** (Client Profile is ok).  
If you run Windows 7 or later, you most likely already have this.


Change Log
===============================

**1.2 - Mar 3, 2014**

 - Credentials now set via the Options dialog instead of directly in the config file, and stored securely using the Windows Data Protection API.

**1.1 - Feb 21, 2014**

 - Added notifications (balloon tips)

**1.0 - Feb 18, 2014**

 - Initial version


To do list
===============================

 - Notification sound
 
 
Want to contribute?
===============================

 - Let me know of any issues you may find
   (you may include details from the log file that is created in the same directory as the executable to help troubleshooting).
 - Fork the code and contribute bug fixes
 
The intent is to keep this app as simple and as close as possible to the original, so there is no plan to implement anything new.


License
===============================

Gmail Notifier Replacement
Copyright (C) 2014 Xavier Poinas

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program. If not, see <http://www.gnu.org/licenses/>.

 [1]: https://developers.google.com/gmail/gmail_inbox_feed
 [2]: https://github.com/xps/Gmail-Notifier-Replacement/releases
 [3]: https://github.com/xps/Gmail-Notifier-Replacement/raw/master/Screenshot.png
 [4]: https://myaccount.google.com/apppasswords
 [5]: http://msdn.microsoft.com/en-us/library/ms995355.aspx
 [6]: https://chocolatey.org/packages/gmailnotifier
