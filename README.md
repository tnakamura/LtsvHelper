# LtsvHelper

A library for reading and writing LTSV file.

## Install

First, install Nuget. Then, install LtsvHelper from the package manager console.

```
PM> Install-Package LtsvHelper
```

## Usage

### reading LTSV file

```c#
using (TextReader textReader = File.OpenRead("some_path.ltsv"))
using (LtsvReader ltsvReader = new LtsvReader(textReader))
{
    while (ltsvReader.Read())
    {
        ltsvReader.GetField("label1");  // => value1
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

