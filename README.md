H# N-Tree

A N-Tree is a tree where each node contains a list of child nodes.  Each leaf contains an list of integers. All data is saved in the leaves. All operations are done in logarithmic time. Building a N-Tree takes linear time. The best size for the list is between 50 and 100 elements. A N-Tree can used with external storage. A N-Tree can be used as a dictionary. A N-Tree is a sorted list.

## Install and Build

The is a C# Console-Mode Project.  Use Visual Studio 2022 and above to compile.  

## Performance

Performance is good. Performance depends on the number of items. Tree Size grows by 60 at each level: 60, 3K, 216K, 12M and 777M.

| Items | Time | Memory | Height |
| --- | --- | --- | --- |
| 1K | 1 ms | 52 mb | 3 |
| 10K | 2 ms | 52 mb | 5 |
| 100K | 4 ms | 52 mb | 5 |
| 1M | 4 ms | 59 mb | 5 |
| 10M | 4 ms | 122 mb | 5 |






















































































