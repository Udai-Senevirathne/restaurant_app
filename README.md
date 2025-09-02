# Restaurant App

A desktop application for restaurant management built with WPF (.NET). It features separate windows for main functionality, admin login, and splash screen, with a flexible MVVM structure.This project is not fully built !!

## Features

- Main application window for customer/order management
- Admin login and admin features
- Splash screen interface
- MVVM architecture (RelayCommand, Models)
- Responsive UI with value converters
- Easily extensible model structure

## Getting Started

### Prerequisites

- Windows OS
- [.NET SDK](https://dotnet.microsoft.com/download) (version compatible with WPF)
- Visual Studio or compatible IDE

### Setup

1. Clone the repository:
   ```sh
   git clone https://github.com/Udai-Senevirathne/restaurant_app.git
   ```
2. Open `restaurent_app.sln` in Visual Studio.
3. Build and run the project.

## Project Structure

- `MainWindow.xaml(.cs)`: Main app window
- `AdminLoginWindow.xaml(.cs)`: Admin login
- `AdminWindow.xaml`: Admin interface
- `SplashScreen.xaml(.cs)`: Splash screen
- `Models/`: Data models
- `RelayCommand.cs`: MVVM command handler
- `WidthRangeConverter.cs`: UI value converter
- `App.xaml(.cs)`: Application resources & startup
- `.gitattributes`, `.gitignore`, `LICENSE.txt`: Configuration & license

## Usage

- Run the app to access restaurant management features.
- Admins can log in for advanced controls.

## License

This project is licensed under the terms of the license in `LICENtSE.tx`.

