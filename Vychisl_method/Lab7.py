import math

n1 = [4, 8, 16]
n2 = [8, 16, 32]

it = 0

while it < len(n1):
    sum1 = 0.
    sum2 = 0.
    h1 = 1 / n1[it]
    h2 = 1 / n2[it]
    r1 = 0.
    r2 = 0.
    for i in range(1, n1[it] + 1):
        sum1 += ((i * h1) ** 3 + ((i - 1) * h1) ** 3) / 2 * h1
    for i in range(1, n2[it] + 1):
        sum2 += ((i * h2) ** 3 + ((i - 1) * h2) ** 3) / 2 * h2
    r1 = 0.25 - sum2
    r2 = (sum2 - sum1) / 3
    print(math.fabs(r1), math.fabs(r2), sep='\n')
    it += 1