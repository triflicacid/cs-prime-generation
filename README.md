# prime-sieve

This directory contains various prime generation methods.

Multiple different methods have been programmed as a CLI application. A GUI is available in `GUI/`, which uses a Sieve of Eratosthenes.

See https://en.wikipedia.org/wiki/Generation_of_primes for more information on the generation of primes.

## Generic

Source: `generic/`

Variation on Euclid's algorithm which calculates the GCD of two numbers to determine if a number is prime. A naive implementation would be as follows:

```
func is_prime(n: int): boolean
  if n <= 1
    return false
  endif

  if n == 2
    return true
  endif

  for i = 3 to n - 1
    if n mod i == 0
      return false
    endif
  endfor

  return true
endfunc
```

This may be improved by immediatly returning false an all multiples of 2 and advancing i by 2 on the for loop.

## Sieve of Eratosthenes

Source: `eratosthenes/`

An advancement on the above algorithm, when the procedure encounters a composite it also removes all numbers divisible by this composite from the domain, thereby reducing the total number of prime checks that must be carried out.

Below is a simple implementation of a sieve which returns an array of all primes up to an input. It first stores an array of whether all integers 2 to n are prime and using the sieve to set composites to false.

```
func is_prime(n: int): boolean
 sieve(n: int): int[]
  boolean[] A = boolean[n - 2]
  A.setAll(true)

  for i = 2 to sqrt(n) - 1
    if A[i] == true
      for j = i * i to n increasing by 2 * i
        A[i] = true
      endfor
    endif
  endfor

  int[] p = int[A.count(true)]
  int j = 0
  for i = 0 to A.length
    if A[i] == true
      A[j] = i
      j = j + 1
    endif
  endfor

  return p
endfunc
```

## Sieve of Sundaram

Source: `sundaram/`

A variant on the Sieve of Eratosthenes which, varies as to its method of removing candidates when a composite $i$ is discovered:
- Eratosthenes: $i^2 + 2i$
- Sundaram: $i + j + 2ij$ where $i := j$ and with $j$ incrementing by one.
