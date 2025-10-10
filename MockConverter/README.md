# How to create a converter

A single converter should support both conversion of continuous values and transient values. It may also support 0..n capabilities such as FastFourierTransformation or BandPassFiltering. 

The converters will be used in two different scenarios:
- First, when importing a file from an instrument (will not use any of the capabilities).
  - Ex: A binary file is placed in an ftp-folder and then the import service will use that file as input to the converter resulting in either a continuous or transient data object.  
- Second, when a modified version of the file is needed (using the capabilities).
  - Ex: Applying a bandpass filter to the transient data in the waveform viewer. 

## Steps to create a converter:
- Create an assembly with a class implementing IConverter
- Implement the specific conversion from the proprietary input formats to the generic Ncvib formats.
- Communicate supported capabilities (if any) using CommonConverterCapabilities and TransientConverterCapabilities
  - Ex: public TransientConverterCapabilities TransientConverterCapabilities => TransientConverterCapabilities.FastFourierTransformation | TransientConverterCapabilities.TimeDomainSwitching;