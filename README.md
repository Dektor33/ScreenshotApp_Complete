# ScreenshotApp

## Overview
ScreenshotApp is a Windows application built with **WPF (C#)** that allows users to capture and manage screenshots easily. Users can take full-screen or selected-area screenshots, save them to a chosen folder, preview them within the app, and send them directly to a **Telegram chat**.

## Features
- 📸 **Full-Screen Screenshot** – Capture the entire screen with one click.
- ✂ **Selected-Area Screenshot** – Manually select a specific area of the screen to capture.
- 💾 **Save Screenshots** – Store images in a user-defined folder.
- 🖼 **Preview Screenshots** – View the captured screenshots within the application.
- 📤 **Send to Telegram** – Automatically send screenshots to a Telegram chat.

## Installation
### Prerequisites
- Windows OS (Windows 10 or later recommended)
- .NET 6.0+ installed ([Download .NET](https://dotnet.microsoft.com/download))
- A **Telegram Bot Token** and **Chat ID** (see [Telegram Bot Setup](#telegram-bot-setup))

### Steps
1. Clone or download the repository:
   ```sh
   git clone https://github.com/yourusername/ScreenshotApp.git
   cd ScreenshotApp
   ```
2. Open the project in **Visual Studio**.
3. Restore NuGet packages if necessary.
4. Replace the following values in `MainWindow.xaml.cs` with your own:
   ```csharp
   private static readonly string botToken = "YOUR_TELEGRAM_BOT_TOKEN";
   private static readonly long chatId = YOUR_CHAT_ID;
   ```
5. Build and run the project.

## Usage
1. **Choose a Folder:** Click the "Choose Folder" button to select where screenshots will be saved.
2. **Take a Screenshot:**
   - Click "Take Screenshot" for a full-screen capture.
   - Click "Take Area Screenshot" to select a specific area.
3. **Preview:** The screenshot will appear in the app window.
4. **Send to Telegram:** Click "Send Screenshot" to upload the latest screenshot to Telegram.

## Telegram Bot Setup
To send screenshots via Telegram, follow these steps:
1. Open **Telegram** and search for **BotFather**.
2. Type `/newbot` and follow the instructions to create a bot.
3. Copy the **bot token** and replace it in `MainWindow.xaml.cs`.
4. Get your **chat ID** by messaging `@userinfobot` and copying the returned ID.

## Dependencies
This project uses the following libraries:
- **Telegram.Bot** – For sending images to Telegram.
- **System.Drawing** – For screenshot capturing.
- **Windows.Forms** – For folder selection and screen area selection.

## Troubleshooting
- Ensure that your Telegram bot token and chat ID are correctly set.
- Check if your Telegram bot allows sending images by trying `/start` in your chat.
- If screenshots don’t appear, verify that the selected folder exists and has write permissions.

## License
This project is licensed under the **Dektor License**. Feel free to modify and share!

## Author
- **Your Name**  
- GitHub: [yourusername](https://github.com/yourusername)
- Telegram: [yourusername](https://t.me/yourusername)

