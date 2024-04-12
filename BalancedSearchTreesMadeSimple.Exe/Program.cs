using BalancedSearchTreesMadeSimple.Lib;

SearchTree<int> tree = new();
List<int> insertedValues = new() { 4, 4, 2, 10, 6, 1, 3, 8, 12, 5, 7, 9, 11, 13 };

//int maxEmpty = tree.Maximum();

foreach (var number in insertedValues)
{
    tree.Insert(number);
}

IEnumerable<int> values = tree.Traverse(OrderEnum.inOrder);
bool contains = tree.Contains(3);
bool containsOther = tree.Contains(-2);
int count = tree.Count();
int max = tree.Maximum();
int min = tree.Minimum();
int countSpecific = tree.Count(4);

foreach (var value in values)
{
    Console.WriteLine(value);
}

Console.WriteLine();