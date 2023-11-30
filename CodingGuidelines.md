# Coding Guidelines

Some basic guidelines to promote code uniformity.

## General Guidelines

1. Prefer .Net system types over C# language types to promote consistent use of 
   Pascal casing for all typenames. I.e. prefer Int32 over int; Double over double.
2. Use var.
3. Use C# 10 global usings and file-scoped namespaces to reduce code boilerplate.
4. Add XML code comments for all public methods of interfaces and public classes.
   If a class implements an interface, do not duplicate the XML comments in the
   interface, use the inheritdoc tag instead. Also update README.md, including examples 
   of usage.
5. Tabs are three (3) spaces.
6. Include benchmarks and include results in README.

## Unit Tests/Naming Conventions

Unit test methods use the following naming convention: 'aaa'_'bbb'_Should'ccc'_When'ddd' where:

* aaa : Class name
* bbb : method name
* ccc : expected result
* ddd : condition being tested

For example, when testing the NextFoo method of class FooBar we would use the following
unit test names.

```
   FooBar_NextFoo_ShouldReturnFalse_WhenNoFoosRemaining
   FooBar_NextFoo_ShouldReturnTrue_WhenOnlyOneFoo
   FooBar_NextFoo_ShouldThrowFooException_WhenInvokedAfterEndOfList
```

The '_When' component may be dropped in cases where forcing the test name to strictly
adhere to the naming convention results in a test name that is less clear than without
the '_When'. For example, instead of:

```
	CheckDigitAlgorithm_Validate_ShouldReturnTrue_WhenCharacterWeightsAreCorrectlyApplied
```

use

```
	CheckDigitAlgirithm_Validate_ShouldApplyCorrectWeightsToEachCharacter
```