﻿#r "bin/Debug/netstandard2.0/Microsoft.FSharpLu.Json.dll"

open Microsoft.FSharpLu.Json

type WithFields = SomeField of int * int
type ComplexDu = ComplexDu of WithFields | SimpleDU | AString of string
let x= (ComplexDu (SomeField (4,9))) |> Default.serialize
let y = x |> Default.deserialize<ComplexDu>
let z = x |> Compact.deserialize<ComplexDu>

module T =
    type OptionOfBase = int option
    let x = Some 5
    Some 5 |> Default.serialize |> Default.deserialize :> OptionOfBase 
    Some 5 |> Default.serialize |> BackwardCompatible.deserialize :> OptionOfBase 
    Default.deserialize<OptionOfBase> "null"
    BackwardCompatible.deserialize<OptionOfBase> "null"

module T2  =
    type X = {Some :string}
    
    Option.Some { X.Some = "test"} |> Compact.serialize

    Option.Some { X.Some = null} |> Compact.serialize

    let z = Option.Some { X.Some = null} |> Compact.serialize |> Compact.deserialize<X option>
    
    Some (Some null) |> Compact.serialize<obj option option> |> Compact.deserialize<obj option option>

    Some null |> Compact.serialize<obj option> |> Compact.deserialize<obj option>

    null  |> Compact.serialize|> Compact.deserialize<obj option option>

