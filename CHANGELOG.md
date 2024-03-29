# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]
## [1.2.5] - 2022-11-01
###
- Simplify interface
## [1.2.4] - 2022-07-15
### Added
- It is possible to set the color names in the palette (editor only)

## [1.2.3] - 2022-07-14
### Fixed
- Fixed a bug that not allowed the ColorLinkSO to be correctly serialized

## [1.2.2] - 2022-07-14
### Changed
- default color for ColorLinkSO is now magenta
### Fixed
- Enable menu to create a PaletteSO from an image
- Fix issue on ImageColorize

## [1.2.1] - 2022-07-14
### Added 
- Menu to duplicate a PaletteSO
- Menu to create a PaletteSO from an image

### Changed
- Using const string to define menu items

## [1.2.0] - 2022-07-14
### Added
- Range of colors to a palette
- More adjectives to describe a color using hue, saturation, and lightness
- Scriptable Object Palette
- Palette now can return a texture with all the colors
- A way to use a Scriptable Object Palette to set colors of Unity components
- PaletteSO has a custom editor with several features:
  - import from texture, 
  - export to texture or color preset, 
  - merge or replace colors from another palette, 
  - reduce number of colors, 
  - sort colors by HSP
  - generate palette from harmonies
  - modifies colors by saturating/desaturating, lightening/darkening, inverting, toning, shading and tinting
- ColorLinkSO has a custom editor to select the linked color


## [1.1.3] - 2022-07-10
### Added
- Color Extensions for Contrast and Hue
- Color Extensions for Saturation and Lightness
- Color Extensions for Color blindness
- Color Extensions for Gradients
- Color Extensions for reducing number of colors to a minimun number or merging together similar colors
- Color Extensions for Lerp (with Tint, Shade and Tones)
- Color Extensions for generating Harmonies
- Color Extensions for Luminance
- Color Extensions for generating Random Color and Harmonies
### Changed
- Split ColorExtensions in multiple files by common theme
- IPalette definition
### Removed
- Removed Palette Generators for simpler extension methods

## [1.1.2] - 2022-07-07
### Added
- Added a Palette data type
- Added 3 generators of Palettes (random, random walk, and harmonic)

## [1.1.1] - 2020-06-28
### Added
- Color Extensions to create color by changing one parameter of the color.
- Helper to use css colors

## [1.1.0] - 2022-06-27
### Breaking Changes
- Requires LiteNinja-Utils
### Added
- HSL & HSV Color Space
- Drawer for HSL & HSV Color



## [0.0.1] - 2022-06-17
Empty project

