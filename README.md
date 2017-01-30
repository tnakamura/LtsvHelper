# LtsvHelper

A library for reading and writing LTSV file.

## Install

First, install Nuget. Then, install LtsvHelper from the package manager console.

```
PM> Install-Package LtsvHelper
```

## Usage

### Reading

#### Reading all records

```c#
using (TextReader textReader = new StreamReader("some_path.ltsv"))
using (LtsvReader ltsvReader = new LtsvReader(textReader))
{
    IEnumerable<MyRecord> records = ltsvReader.GetRecords<MyRecord>();
}
```

#### Reading records manualy

```c#
using (TextReader textReader = new StreamReader("some_path.ltsv"))
using (LtsvReader ltsvReader = new LtsvReader(textReader))
{
    while (ltsvReader.Read())
    {
        MyRecord records = ltsvReader.GetRecord<MyRecord>();
    }
}
```

#### Reading individual fields

```c#
using (TextReader textReader = new StreamReader("some_path.ltsv"))
using (LtsvReader ltsvReader = new LtsvReader(textReader))
{
    while (ltsvReader.Read())
    {
        string value1 = ltsvReader.GetField("label1");
        int value2 = ltsvReader.GetField<int>("label2);
    }
}
```

### Writing

#### Writing records manualy

```c#
using (TextWriter textWriter = new StreamWriter("some_path.ltsv"))
using (LtsvWriter ltsvWriter = new LtsvWriter(textWriter))
{
    foreach (var item in list)
    {
        ltsvWriter.WriteRecord(item);
    }
}
```

#### Writing individual fields

```c#
using (TextWriter textWriter = new StreamWriter("some_path.ltsv"))
using (LtsvWriter ltsvWriter = new LtsvWriter(textWriter))
{
    foreach (var item in list)
    {
        ltsvWriter.WriteField("label1", "value1");
        ltsvWriter.WriteField("label2", 10);
        ltsvWriter.NextRecord();
    }
}
```

### Mapping

#### Fluent Class Mapping

```c#
public class MyClassMap : LtsvClassMap<MyClass>
{
    public MyClassMap()
    {
        Map(m => m.Id);
        Map(m => m.Name);
    }
}
```

##### Label

```c#
public class MyClassMap : LtsvClassMap<MyClass>
{
    public MyClassMap()
    {
        Map(m => m.Id).Label("id");
        Map(m => m.Name).Label("name");
    }
}
```

## Contribution

1. Fork it
2. Create your feature branch ( git checkout -b my-new-feature )
3. Commit your changes ( git commit -am 'Add some feature' )
4. Push to the branch ( git push origin my-new-feature )
5. Create new Pull Request

## License

[MIT](https://raw.githubusercontent.com/tnakamura/LtsvHelper/master/LICENSE)

## Author

[tnakamura](https://github.com/tnakamura)

