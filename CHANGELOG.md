# CHANGELOG

## SubSync 1.0 (Stable)

First **Stable** release! Thanks everyone for the feedback during Beta Stage! :)

- Internet connection check during startup and regularly, with GUI messages in case the connection has been lost, as well as stopping automatic subtitle download
- Fixed issue that prevented the application from starting when old media folders are not present anymore on the file system
- Included missing pt-BR translations
- Minor UI enhancements and bugfixes found on Beta

## SubSync Beta 0.8

News:

- Olá amigos! SubSync is now translated in PT-BR, the most awesome language ever!
- A new option for choosing the desired UI language (currently EN or PT-BR)
- Some minor bug fixes, UI enhancements and code cleaning

Notes:

- Only this time the new version won't remember your preferences from the old version. That's due to some one-time assembly changes that messed up with application data folder location

## SubSync Beta 0.7

News:

- First Beta release! All features that will ship into 1.0 version are included
- Release wrapped into a Setup for an easier installation
- Automatic and manual check for application updates
- "About" window with application information
- Multiple application instances prevention
- New application Icon and Logo, by Diogo Cezar
- Many bug fixes, UI enhancements and code cleaning

Notes:

- Please delete old SubSync package prior to Setup installation
- Setup package is available in both English and Portuguese languages, but the program itself was not yet translated into Portuguese
- Updates checking will run automatically when the program starts and then in every 12 hours

## SubSync Alpha 0.6.150207

New features:
- Program now saves the user preferences for the selected media folders and preferred languages and remembers that on application restart
- Option for automatic initialization at windows logon
- When initialized with windows logon, the program automatically starts the media folders monitoring and subtitle download

Notes:
- The user preferences gets cleaned if you move the application executable location

## SubSync Alpha 0.5.150201

New features:
- Automatic check for missing subtitles when the synchronization starts
- Scheduled check for missing subtitles every 30 minutes
- Option to check for missing subtitles at any time, on the tray icon's menu
- "Open SubSync" and "Start/Stop SubSync" options included on the tray icon's menu
- The program now shows for what video file the subtitle has been downloaded for, on the balloon notification

Bugs fixed:
- Subtitle formats were being always saved as SubRip's (.srt) format. Now the program verifies the downloaded format - SubRip (.srt) or SubViewer (.sub) - - and saves the filename properly.
- In some cases, the program was still monitoring directories previously removed from the list
- SubSync.exe background process was being kept running after the user exited the applicaiton

## SubSync Alpha 0.4.1.150123

Bugs fixed:
- SubSync was not correctly downloading subtitles for the second choice languages, in case the subtitle provider did not have the subtitle available for the first choice.

Current features:
- Main window on which you can setup your media folders and preferred languages
- Media folders monitoring for new videos
- Subfolders monitoring
- Automatic subtitles download from SubDB subtitles database when new files are detected
- "Hide to tray" by closing the main window
- Restore app window from tray by double clicking on the application Icon from tray area
- Right click menu on the application's icon on tray area with "Exit" option
- Support for files/directories moving into the monitored folders (only copying was working well)

## SubSync Alpha 0.4.150117

Features included:
- Support for files/directories moving into the monitored folders (only copying was working well).

Bugs fixed:
- Fix for problem that was preventing subsync to download all subtitles when a lot of media files were being copied/moved into the monitored folders at the - same time (only a few subs were being fetched).

Previous features:
- Main window on which you can setup your media folders and preferred languages
- Media folders monitoring for new videos
- Subfolders monitoring
- Automatic subtitles download from SubDB subtitles database when new files are detected
- "Hide to tray" by closing the main window
- Restore app window from tray by double clicking on the application Icon from tray area
- Right click menu on the application's icon on tray area with "Exit" option

## SubSync Alpha 0.3.150116

Features included:
- "Hide to tray" by closing the main window
- Restore app window from tray by double clicking on the application Icon from tray area
- Right click menu on the application's icon on tray area with "Exit" option

Fixes:
- Fix for window freeze when the program takes a long time to start monitoring the media folders
- Fix for the scenario when windows provides the video libraries folders as network paths (UNC) instead of regular drive paths

Previous features:
- Main window on which you can setup your media folders and preferred languages
- Media folders monitoring for new videos
- Subfolders monitoring
- Automatic subtitles download from SubDB subtitles database when new files are detected

Notes:
- The program is not yet checking pre-existent files in the media folders nor is checking regularly for new subtitles availability.

## SubSync Alpha 0.2.150112

Minor feature included:
- Now SubSync also monitors subfolders of the media folders specified on the main window. Not tested throughly, but shouldn't present major issues in most cases.

Previous features:
- Main window on which you can setup your media folders and preferred languages
- Media folders monitoring for new videos
- Automatic subtitles download from SubDB subtitles database when new files are detected

Notes:
- The program is not yet checking pre-existent files in the media folders nor is checking regularly for new subtitles availability.

Technical notes:
- All process that is executed when a new video file is found has been moved to a asynchronous Task, in order to avoid buffering issues with the FileSystem - Watchers when they are also scanning subfolders.

## SubSync Alpha 0.1.150112 - First Alpha Release!

Features included:
- Main window on which you can setup your media folders and preferred languages
- Media folders watching for new videos
- Automatic subtitles download from SubDB subtitles database when new files are detected

Tested features:
- If you copy video files from another folder to a watched folder, the app should download the subtitles right away
- If you include your torrent download folder as a watcher folder, the app will download the subtitles as soon as possible

Notes:
- "My videos" and every Video library folder setup on windows are automatically included. You can remove them as you wish
- System language is automatically included on the preferred list of languages
- Even though the app shows up on the tray area, if you close the main windows the program will close. If you minimize it, it won't hide to the tray icon.
