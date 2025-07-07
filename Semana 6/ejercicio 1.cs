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

int contarElementos(Nodo* cabeza) {
    int contador = 0;
    Nodo* actual = cabeza;
    while (actual != NULL) {
        contador++;
        actual = actual->siguiente;
    }
    return contador;
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
    lista = agregar(lista, 5);
    lista = agregar(lista, 10);
    lista = agregar(lista, 15);

    imprimir(lista);
    printf("Número de elementos: %d\n", contarElementos(lista));
    return 0;
}
