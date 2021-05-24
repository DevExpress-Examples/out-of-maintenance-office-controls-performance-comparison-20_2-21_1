# RichEdit and Spredsheet Controls Performance Comparison 20.2 with 21.1

You can use this repository to compare performance of RichEdit and Spreadsheet controls (v20.2 vs v21.1). Run the RibbonPerformanceComparison project and select a control to measure the control performance. Note that we have used the 'release' build configuration to benchmark controls' performance. 

After you have done benchmarks, refer to the following text files to get the results: 

| Control| Version | With\Without Document | File Path |
|-|-|-|-|
|RichEdit|20.2|With Document|/CS/bin/RichEdit20_2_WithoutDocument.txt|
|RichEdit|20.2|Without Document|/CS/bin/RichEdit20_2_WithDocument.txt|
|RichEdit|21.1|With Document|/CS/bin/RichEdit21_1_WithoutDocument.txt|
|RichEdit|21.1|Without Document|/CS/bin/RichEdit21_1_WithDocument.txt|
|Spreadsheet|20.2|With Document|/CS/bin/Spreadsheet20_2_WithoutDocument.txt|
|Spreadsheet|20.2|Without Document|/CS/bin/Spreadsheet20_2_WithDocument.txt|
|Spreadsheet|21.1|With Document|/CS/bin/Spreadsheet21_1_WithoutDocument.txt|
|Spreadsheet|21.1|Without Document|/CS/bin/Spreadsheet21_1_WithDocument.txt|

The first column in a result file is a cold startup time, the second one is the hot startup time. 

Refer to the following documentation topic for more information on the Ribbon performance: [Performance Enhancements](https://docs.devexpress.com/WPF/403033/controls-and-libraries/ribbon-bars-and-menu/ribbon/performance-enhancements?v=21.1)
