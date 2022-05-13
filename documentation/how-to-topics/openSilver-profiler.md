## Measure performance with OpenSilver's Profiler class.

OpenSilver provides the OpenSilver.Profiler class which allows you to measure the time spent in specific portions of your code.

#### 1. Define the portions of code you would like to measure

The OpenSilver.Profiler class lets you use the two following methods:
- ***OpenSilver.Profiler.StartMeasuringTime()***

    You can call this method before the code that you wish to profile in order to start the stopwatch.

    This method returns a number that you need to pass to the "StopMeasuringTime()" method.
- ***OpenSilver.Profiler.StopMeasuringTime(description, resultOfStartMeasureCall)***

    You can call this method after the code that you wish to profile, in order to stop the stopwatch.

    This method takes 2 arguments: the first is an arbitrary text that you can specify to describe the measure, while the second argument is the number returned by the call to the "StopMeasuringTime()" method.

***Note:*** the time measured is "accrued", meaning that it is cumulative if the code is executed multiple times.

<!--The following is to have a space before the next line.-->
####

***Here is an example of use:***

```
// Call the "StartMeasuringTime" method to start the stopwatch:
var t0 = OpenSilver.Profiler.StartMeasuringTime();

// This is the code that we want to profile (ie. measure the time it takes to execute):
List<string> list = new List<string>();
for (int i = 0; i < 10000; i++)
{
    list.Add(i.ToString());
}

// Call the "StopMeasuringTime" method to stop the stopwatch:
OpenSilver.Profiler.StopMeasuringTime("Time it takes to execute a loop with 10000 items", t0);
```

#### 2. View the results:

- ***In the Browser:***
    1. Open the Browser's "Developer tools" (you can usually do so by pressing F12 or Ctrl+Shift+I).

    2. Open the Console window by clicking the "Console" tab in the Developer tools.

    3. Type the following JavaScript inside the Console window then press enter:
        ```
        window.ViewProfilerResults()
        ```
- ***In the Simulator:***

    1. Click the tool icon at the bottom-left of the Simulator then click the "Execute custom JavaScript code" option.

    2. In the window that appears type the following code, then press OK:
        ```
        window.ViewProfilerResults()
        ```

    3. Go to the "Output" window in Visual Studio.


***Results:***

 The list of all the measurements will appear, together with the total time taken for each of them, the number of calls, and the average time per call.

You will also get a CSV-formatted output that is useful for copy/pasting into a spreadsheet application. This is how it works to copy/paste into a spreadsheet application while preserving the format of the data:

- With Microsoft Excel, you can copy/paste the CSV-formatted output into a new spreadsheet, then go to the Data menu, and click the "Text to Columns" button. A wizard will appear, choose "Delimited", click Next, choose "Comma" for the delimiter, and click Finish.
- With Google Spreadsheets (online), you can copy/paste the CSV-formatted output into a new spreadsheet, then click the small contextual button that appears after pasting, and click "Split text to columns..."