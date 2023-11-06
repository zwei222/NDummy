# NDummy
`NDummy` is a simple, yet powerful command-line tool designed to generate dummy files with specified sizes, useful for testing, developing, and benchmarking purposes. Unlike the `fsutil` command available in Windows, which creates dummy files with zeroed bytes, `NDummy` fills files with random content, ensuring realistic file density and characteristics.

## Features
- __Custom File Sizes__: Create files of any specified size using bytes, KB, MB, GB, etc.
- __Random Content__: Generated files are populated with random data to simulate real-world usage and ensure unique file content.
- __Overwrite Capability__: Allows for existing files to be overwritten if required.

## Usage
- `-o` , `--output`: Specifies the file path where the dummy file will be generated. If not provided, the file will be created in the current directory.
- `--overwrite`: If the output file already exists, this option will allow NDummy to overwrite the existing file.

## Examples
Generate a 1GB dummy file:

```
NDummy 1GB -o C:\path\to\file\1GB.bin
```

Generate a 512KB dummy file and overwrite if it already exists:

```
NDummy 512KB -o .\512KB.bin --overwrite
```

# Author
[@zwei_222](https://twitter.com/zwei_222)

# License
This software is released under the MIT License, see LICENSE.
