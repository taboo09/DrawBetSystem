# Draw Betting System Web App
This app is based on Martingale system.
The Martingale system is a well-known roulette strategy. The system is clear, easy to use and it really makes sense. Other systems however require a good deal of strategic and mathematic understanding – qualities that not everybody is blessed with (or have the patience for). To use the Martingale correctly you don’t even need to take notes, the concept behind it is easily understood. Nevertheless it involves a lot of risks...

## How the Draw Betting System Works
The concept is quite simple, you choose a team and place DRAW bets. After every bet you lose, you double your stake, and you keep doing that until you win. The first win will recover all previous losses, plus give you a profit equal to your original bet. At this point you start all over again with your original stake, which you double again until your next win. Keep doing this and you'll get a nice profit!

## Run the App
The app is developed in Microsoft AspNetCore 2 MVC.
Copy the files, install packages, enable EF Migrations and run a initial migration to create the database tables.
From terminal or command prompt type `dotnet restore`.
Finally type `dotnet run`.

## How it works
1. Add a season, from Admin page. This option allows you to add teams and bets.
2. Add some teams you want to bet on.
3. Add bets on your added teams. (Note: the app is based on DRAW bets!)

You can add, edit or delete bets and visualizing a bunch of statistics for teams, seasons, each team's profit, money-in and so on...

# Good Luck!
