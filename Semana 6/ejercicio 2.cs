#include <stdio.h>
#include <stdlib.h>

typedef struct Nodo {
    int dato;
    struct Nodo* siguiente;
} Nodo;

Nodo* agregar(Nodo* cabeza, int dato) {
    Nodo* nuevo = (Nodo*)malloc(sizeof(Nodo));
    nuevo->dato = dato;
    nuevo->siguiente = cabeza;
    return nuevo;
}

Nodo* invertir(Nodo* cabeza) {
    Nodo* anterior = NULL;
    Nodo* actual = cabeza;
    Nodo* siguiente = NULL;

    while (actual != NULL) {
        siguiente = actual->siguiente;
        actual->siguiente = anterior;
        anterior = actual;
        actual = siguiente;
    }
    return anterior;
}

void imprimir(Nodo* cabeza) {
    Nodo* actual = cabeza;
    while (actual != NULL) {
        printf("%d -> ", actual->dato);
        actual = actual->siguiente;
    }
    printf("NULL\n");
}

int main() {
    Nodo* lista = NULL;
    lista = agregar(lista, 1);
    lista = agregar(lista, 2);
    lista = agregar(lista, 3);
    lista = agregar(lista, 4);

    printf("Original:\n");
    imprimir(lista);

    lista = invertir(lista);

    printf("Invertida:\n");
    imprimir(lista);
    return 0;
}
