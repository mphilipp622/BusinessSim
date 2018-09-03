using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class DescendingComparer<T> : IComparer<T>
{
	/// <summary>
	/// Used for sorted dictionaries. Will sort in descending order instead of ascending.
	/// Used for Consumer demand functionality.
	/// </summary>
	private readonly IComparer<T> original;

	public DescendingComparer(IComparer<T> original)
	{
		// TODO: Validation
		this.original = original;
	}

	public int Compare(T left, T right)
	{
		return original.Compare(right, left);
	}
}
