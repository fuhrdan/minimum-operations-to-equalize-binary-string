//*****************************************************************************
//** 3666. Minimum Operations to Equalize Binary String             leetcode **
//*****************************************************************************
//** We count the zeros, chart parity’s domain,
//** Flip k at a time through a narrowing plane.
//** While union find leaps we skip what we’ve seen,
//** Till zero hits zero then all turns to one, clean.
//*****************************************************************************
//** Then and than are different, but this code is the same.

static int find(int* parent, int x)
{
    if (parent[x] != x)
    {
        parent[x] = find(parent, parent[x]);
    }
    return parent[x];
}

int minOperations(char* s, int k)
{
    int n = strlen(s);
    int i;

    int cnt0 = 0;
    for (i = 0; i < n; i++)
    {
        if (s[i] == '0')
        {
            cnt0++;
        }
    }

    if (cnt0 == 0)
    {
        return 0;
    }

    /* parent array for DSU skipping */
    int* parent = (int*)malloc((n + 3) * sizeof(int));
    for (i = 0; i <= n + 2; i++)
    {
        parent[i] = i;
    }

    /* visited handled implicitly by DSU */
    int* queue = (int*)malloc((n + 1) * sizeof(int));
    int front = 0;
    int rear = 0;

    queue[rear++] = cnt0;

    /* mark cnt0 as visited */
    parent[cnt0] = cnt0 + 2;

    int ans = 0;

    while (front < rear)
    {
        int size = rear - front;

        while (size > 0)
        {
            int cur = queue[front++];
            size--;

            if (cur == 0)
            {
                free(parent);
                free(queue);
                return ans;
            }

            int minFlip = (cur < k) ? cur : k;
            int maxFlip = (k - n + cur > 0) ? (k - n + cur) : 0;

            int l = cur + k - 2 * minFlip;
            int r = cur + k - 2 * maxFlip;

            int start = find(parent, l);

            while (start <= r)
            {
                queue[rear++] = start;

                /* mark visited: union with start+2 */
                parent[start] = find(parent, start + 2);

                start = find(parent, start);
            }
        }

        ans++;
    }

    free(parent);
    free(queue);
    return -1;
}