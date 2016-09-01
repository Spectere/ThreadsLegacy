# Threads

## Overview

*Threads* is an application designed to allow users to create *Choose Your Own Adventure*-style stories.

## Requirements

You must have Microsoft Visual Studio (or a compatible IDE) with .NET 4.6.1 support in order to build *Threads*. It was designed using Visual Studio 2015 Community Edition, but you should be able to build it with versions as low as 2012 as long as you have the [Microsoft .NET 4.6.1 Developer Pack](https://www.microsoft.com/en-us/download/details.aspx?id=49978) installed on your system.

Since it is a WPF application, it is also bound to Windows. **Threads.Interpreter** and **Threads.Markup** are not dependend on WPF in any way, so it should be relatively simple to reimplement **Threads.Player** using a different toolkit.

### Why WPF?

I started *Threads* in order to learn WPF. Odds are, any rough spots in the UI code can be attributed to that simple fact. :)

## Contributing

Contributions of all forms are both welcome and helpful! If you would like to see any of the code changed, feel free to submit a pull request and I'll merge it if I like it. If you're not a coder, please feel free to submit issues for both feature requests and bugs.

If you would like to submit issues but do not wish to create a GitHub account to do so, feel free to send me a message on [Twitter](https://twitter.com/Spectere).

And finally, if you use *Threads* to write any stories, please send them my way! I'd love to be able to bundle some actual stories rather than a handful of test files.

## License

*Threads*, and all of the applications included in its repository, are released as free software released under the [MIT license](Documentation/LICENSE.txt).
