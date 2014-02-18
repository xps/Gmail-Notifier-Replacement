What is it?
===============================

This is an open-source alternative to the official Google Gmail Notifier (now retired).

Features
===============================

It is largely incomplete at this point. Currently it will only show the mail icon in the system tray,
turning blue when you have unread mail. Hovering over the icon will show a tooltip letting you know
how many unread emails you have.

It will check mail every couple of minutes via [Gmail's Atom Feed][1].

**Note:** This feed is only available for Gmail accounts on Google Apps domains.

To do list
===============================

 - Options dialog
 - Sounds
 - Popup notifications
 - Installer

How to use?
===============================

Download, unzip, and edit the config file to set your username and password.

You will need to **generate an application-specific password** in your Google Account, under the Security > Password > App Passwords section.
It will not work with your real password.

Then start the exe file (or set up Windows to start it up for you automatically).

**Requires the .NET Framework 3.5 or above** (Client Profile is ok).

Download
===============================

Currently only available in source form.

Want to contribute?
===============================

 - Let me know of any issues you may find
   (you may include details from the log file that is created in the same directory as the executable to help troubleshooting).
 - Fork the code and contribute bug fixes or new features.

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