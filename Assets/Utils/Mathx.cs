using System;
using UnityEngine;

public static class Mathx {
	////////////////////////////
	// --- Wrap Functions --- //
	////////////////////////////

	public static sbyte Wrap(sbyte n, sbyte a, sbyte b) {
		if (n < a)
			n = (sbyte)(b - (a - n) % (b - a));
		else
			n = (sbyte)(a + (n - a) % (b - a));
		
		return n;
	}
	
	public static sbyte Wrap(sbyte n, sbyte m) {
		return Wrap(n, (sbyte)0, (sbyte)(m - 1));
	}

	public static short Wrap(short n, short a, short b) {
		if (n < a)
			n = (short)(b - (a - n) % (b - a));
		else
			n = (short)(a + (n - a) % (b - a));
		
		return n;
	}
	
	public static short Wrap(short n, short m) {
		return Wrap(n, (short)0, (short)(m - 1));
	}

	public static int Wrap(int n, int a, int b) {
		if (n < a)
			n = b - (a - n) % (b - a);
		else
			n = a + (n - a) % (b - a);
		
		return n;
	}
	
	public static int Wrap(int n, int m) {
		return Wrap(n, 0, m - 1);
	}

	public static long Wrap(long n, long a, long b) {
		if (n < a)
			n = b - (a - n) % (b - a);
		else
			n = a + (n - a) % (b - a);
		
		return n;
	}

	public static long Wrap(long n, long m) {
		return Wrap(n, 0L, m - 1L);
	}

	public static byte Wrap(byte n, byte a, byte b) {
		return (byte)(n % (b - a) + a);
	}

	public static byte Wrap(byte n, byte m) {
		return Wrap(n, (byte)0, (byte)(m - 1));
	}

	public static ushort Wrap(ushort n, ushort a, ushort b) {
		return (ushort)(n % (b - a) + a);
	}

	public static ushort Wrap(ushort n, ushort m) {
		return Wrap(n, (ushort)0, (ushort)(m - 1));
	}

	public static uint Wrap(uint n, uint a, uint b) {
		return n % (b - a) + a;
	}

	public static uint Wrap(uint n, uint m) {
		return Wrap(n, 0u, m - 1u);
	}

	public static ulong Wrap(ulong n, ulong a, ulong b) {
		return n % (b - a) + a;
	}

	public static ulong Wrap(ulong n, ulong m) {
		return Wrap(n, 0Lu, m - 1Lu);
	}

	/////////////////////////////
	// --- Clamp Functions --- //
	/////////////////////////////

	public static sbyte Clamp(sbyte n, sbyte a, sbyte b) {
		return Math.Min(Math.Max(n, a), b);
	}

	public static short Clamp(short n, short a, short b) {
		return Math.Min(Math.Max(n, a), b);
	}

	public static int Clamp(int n, int a, int b) {
		return Math.Min(Math.Max(n, a), b);
	}

	public static long Clamp(long n, long a, long b) {
		return Math.Min(Math.Max(n, a), b);
	}

	public static byte Clamp(byte n, byte a, byte b) {
		return Math.Min(Math.Max(n, a), b);
	}

	public static ushort Clamp(ushort n, ushort a, ushort b) {
		return Math.Min(Math.Max(n, a), b);
	}

	public static uint Clamp(uint n, uint a, uint b) {
		return Math.Min(Math.Max(n, a), b);
	}

	public static ulong Clamp(ulong n, ulong a, ulong b) {
		return Math.Min(Math.Max(n, a), b);
	}
}
