#include <stdio.h>
#include <stdlib.h>

#define MAX_DISKS 10

typedef struct {
    int disks[MAX_DISKS];
    int top;
    char name;
} Tower;

// Inicializa una torre
void initTower(Tower *t, char name) {
    t->top = -1;
    t->name = name;
}

// Inserta un disco en la torre
void push(Tower *t, int disk) {
    if (t->top < MAX_DISKS - 1) {
        t->disks[++t->top] = disk;
    }
}

// Saca un disco de la torre
int pop(Tower *t) {
    if (t->top >= 0) {
        return t->disks[t->top--];
    }
    return -1;
}

// Imprime el movimiento
void moveDisk(Tower *from, Tower *to) {
    int disk = pop(from);
    push(to, disk);
    printf("Mover disco %d de %c a %c\n", disk, from->name, to->name);
}

// Algoritmo recursivo de Hanoi
void solveHanoi(int n, Tower *source, Tower *aux, Tower *dest) {
    if (n == 1) {
        moveDisk(source, dest);
    } else {
        solveHanoi(n - 1, source, dest, aux);
        moveDisk(source, dest);
        solveHanoi(n - 1, aux, source, dest);
    }
}

int main() {
    int n;
    printf("Ingrese el número de discos: ");
    scanf("%d", &n);

    Tower A, B, C;
    initTower(&A, 'A');
    initTower(&B, 'B');
    initTower(&C, 'C');

    // Llenar la torre A
    for (int i = n; i >= 1; i--) {
        push(&A, i);
    }

    printf("\nMovimientos para resolver Torres de Hanoi con %d discos:\n", n);
    solveHanoi(n, &A, &B, &C);

    return 0;
}
