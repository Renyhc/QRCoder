﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QRCoder
{
    public partial class QRCodeGenerator
    {
        /// <summary>
        /// Represents a polynomial, which is a sum of polynomial terms.
        /// </summary>
        private struct Polynom : IDisposable
        {
            private PolynomItem[] _polyItems;
            private int _length;

            /// <summary>
            /// Initializes a new instance of the <see cref="Polynom"/> struct with a specified number of initial capacity for polynomial terms.
            /// </summary>
            /// <param name="count">The initial capacity of the polynomial items list.</param>
            public Polynom(int count)
            {
                _length = 0;
                _polyItems = Allocate(count);
            }

            /// <summary>
            /// Adds a polynomial term to the polynomial.
            /// </summary>
            public void Add(PolynomItem item)
            {
                EnsureCapacity(_length + 1);
                _polyItems[_length++] = item;
            }

            /// <summary>
            /// Removes the polynomial term at the specified index.
            /// </summary>
            public void RemoveAt(int index)
            {
                if (index < 0 || index >= _length)
                    throw new IndexOutOfRangeException();

                if (index < _length - 1)
                    Array.Copy(_polyItems, index + 1, _polyItems, index, _length - index - 1);

                _length--;
            }

            /// <summary>
            /// Gets or sets a polynomial term at the specified index.
            /// </summary>
            public PolynomItem this[int index]
            {
                get {
                    if (index < 0 || index >= _length)
                        throw new IndexOutOfRangeException();
                    return _polyItems[index];
                }
                set {
                    if (index < 0 || index >= _length)
                        throw new IndexOutOfRangeException();
                    _polyItems[index] = value;
                }
            }

            /// <summary>
            /// Gets the number of polynomial terms in the polynomial.
            /// </summary>
            public int Count => _length;

            /// <summary>
            /// Removes all polynomial terms from the polynomial.
            /// </summary>
            public void Clear()
            {
                _length = 0;
            }

            /// <summary>
            /// Clones the polynomial, creating a new instance with the same polynomial terms.
            /// </summary>
            public Polynom Clone()
            {
                var newPolynom = new Polynom(_length);
                Array.Copy(_polyItems, newPolynom._polyItems, _length);
                newPolynom._length = _length;
                return newPolynom;
            }

            /// <summary>
            /// Sorts the collection of <see cref="PolynomItem"/> using a custom comparer function.
            /// </summary>
            /// <param name="comparer">
            /// A function that compares two <see cref="PolynomItem"/> objects and returns an integer indicating their relative order:
            /// less than zero if the first is less than the second, zero if they are equal, or greater than zero if the first is greater than the second.
            /// </param>
            public void Sort(Func<PolynomItem, PolynomItem, int> comparer)
            {
                if (comparer == null) throw new ArgumentNullException(nameof(comparer));

                var items = _polyItems;
                if (items == null || _length <= 1)
                {
                    return; // Nothing to sort if the list is empty or contains only one element
                }

                void QuickSort(int left, int right)
                {
                    int i = left;
                    int j = right;
                    PolynomItem pivot = items[(left + right) / 2];

                    while (i <= j)
                    {
                        while (comparer(items[i], pivot) < 0) i++;
                        while (comparer(items[j], pivot) > 0) j--;

                        if (i <= j)
                        {
                            // Swap items[i] and items[j]
                            PolynomItem temp = items[i];
                            items[i] = items[j];
                            items[j] = temp;
                            i++;
                            j--;
                        }
                    }

                    // Recursively sort the sub-arrays
                    if (left < j) QuickSort(left, j);
                    if (i < right) QuickSort(i, right);
                }

                QuickSort(0, _length - 1);
            }

            /// <summary>
            /// Returns a string that represents the polynomial in standard algebraic notation.
            /// Example output: "a^2*x^3 + a^5*x^1 + a^3*x^0", which represents the polynomial 2x³ + 5x + 3.
            /// </summary>
            public override string ToString()
            {
                var sb = new StringBuilder();

                foreach (var polyItem in _polyItems)
                {
                    sb.Append("a^" + polyItem.Coefficient + "*x^" + polyItem.Exponent + " + ");
                }

                // Remove the trailing " + " if the string builder has added terms
                if (sb.Length > 0)
                    sb.Length -= 3;

                return sb.ToString();
            }

            /// <inheritdoc/>
            public void Dispose()
            {
                Free(_polyItems);
                _polyItems = null;
            }

            /// <summary>
            /// Ensures that the polynomial has enough capacity to store the specified number of polynomial terms.
            /// </summary>
            private void EnsureCapacity(int min)
            {
                if (_polyItems.Length < min)
                {
                    // All math by QRCoder should be done with fixed polynomials, so we don't need to grow the capacity.
                    ThrowNotSupportedException();

                    // Sample code for growing the capacity:
                    //var newArray = Allocate(Math.Max(min - 1, 8) * 2); // Grow by 2x, but at least by 8
                    //Array.Copy(_polyItems, newArray, _length);
                    //Free(_polyItems);
                    //_polyItems = newArray;
                }

                void ThrowNotSupportedException()
                {
                    throw new NotSupportedException("The polynomial capacity is fixed and cannot be increased.");
                }
            }

#if NETCOREAPP
            /// <summary>
            /// Allocates memory for the polynomial terms.
            /// </summary>
            private static PolynomItem[] Allocate(int count)
            {
                return System.Buffers.ArrayPool<PolynomItem>.Shared.Rent(count);
            }

            /// <summary>
            /// Frees memory allocated for the polynomial terms.
            /// </summary>
            private static void Free(PolynomItem[] array)
            {
                System.Buffers.ArrayPool<PolynomItem>.Shared.Return(array);
            }
#else
            // Implement a poor-man's array pool for .NET Framework
            [ThreadStatic]
            private static List<PolynomItem[]> _arrayPool;

            /// <summary>
            /// Allocates memory for the polynomial terms.
            /// </summary>
            private static PolynomItem[] Allocate(int count)
            {
                if (count <= 0)
                    ThrowArgumentOutOfRangeException();

                // Search for a suitable array in the thread-local pool, if it has been initialized
                if (_arrayPool != null)
                {
                    for (int i = 0; i < _arrayPool.Count; i++)
                    {
                        var array = _arrayPool[i];
                        if (array.Length >= count)
                        {
                            _arrayPool.RemoveAt(i);
                            return array;
                        }
                    }
                }

                // No suitable buffer found; create a new one
                return new PolynomItem[count];

                void ThrowArgumentOutOfRangeException()
                {
                    throw new ArgumentOutOfRangeException(nameof(count), "The count must be a positive number.");
                }
            }

            /// <summary>
            /// Frees memory allocated for the polynomial terms.
            /// </summary>
            private static void Free(PolynomItem[] array)
            {
                if (array == null)
                    ThrowArgumentNullException();

                // Initialize the thread-local pool if it's not already done
                if (_arrayPool == null)
                    _arrayPool = new List<PolynomItem[]>(8);

                // Add the buffer back to the pool
                _arrayPool.Add(array);

                void ThrowArgumentNullException()
                {
                    throw new ArgumentNullException(nameof(array));
                }
            }
#endif

            /// <summary>
            /// Returns an enumerator that iterates through the polynomial terms.
            /// </summary>
            public PolynumEnumerator GetEnumerator() => new PolynumEnumerator(this);

            /// <summary>
            /// Value type enumerator for the <see cref="Polynom"/> struct.
            /// </summary>
            public struct PolynumEnumerator
            {
                private Polynom _polynom;
                private int _index;

                public PolynumEnumerator(Polynom polynom)
                {
                    _polynom = polynom;
                    _index = -1;
                }

                public PolynomItem Current => _polynom[_index];

                public bool MoveNext() => ++_index < _polynom._length;
            }
        }
    }
}
