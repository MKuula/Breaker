# Breaker
Keeping breaks during a workday will prevent you from getting aches of musculoskeletals. The best way to remember breaks is to enslave your computer to tell you when the break should be kept. The job is achieved by an application I introduce here. The original program was made using C# and Visual Studio 2013 Community Edition, later updated with VS 2022.

How often the message should be seen? I recommend after half hour.

You can pause the application, too. It could be useful if you are sharing your screen for other people. This is the only option which our version has so it has a superior user interface: it lacks the UI almost entirely.

We take advantage of notification area, known also as a system tray, which is usually located at the bottom right corner of the screen. Our program stays inconspicuous until the break moment approaches. Then it shows a balloon reminder. Balloon reminders have at least one advantage over "normal" message boxes: they will not interfere the task being done at the computer. For instance, if you are writing some report and a message window appears, you will lose some key presses because the message window "steals" them.

One final thing to consider: how could our program know when the timer should be reset? We have some options: fixed time intervals, an acknowledgement button or when the user leaves the computer. First one does not cover the employers which use flextime nor pay attention to events of a day. The second one requires that the user remembers to press the key or keystroke. So, I suggest that we choose the last option.

Great, but in which situations our program would know that the user left the computer? When the one locked the computer. I trust everybody leaving their work computers lock the Windows. I could be wrong, though. Turning the timer on or off is done at the handler starting in line 32 (the routine and most of the functional code is located in the file HiddenForm.cs).

![image](https://github.com/MKuula/Breaker/assets/168563015/566bea2d-78ec-49f1-8d82-8bc0a5c2d8a7)

![image](https://github.com/MKuula/Breaker/assets/168563015/8d35e11e-4030-438c-8c8b-960c08d354f4)
