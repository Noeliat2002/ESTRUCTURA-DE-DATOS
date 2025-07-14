#include <stdio.h>
#include <stdlib.h>

#define MAX 100

// Stack tipo char
typedef struct {
    char data[MAX];
    int top;
} CharStack;

// Inicializa el stack
void initStack(CharStack *s) {
    s->top = -1;
}

// Agrega un carácter al stack
void push(CharStack *s, char ch) {
    if (s->top < MAX - 1) {
        s->data[++s->top] = ch;
    }
}

// Saca un carácter del stack
char pop(CharStack *s) {
    if (s->top >= 0) {
        return s->data[s->top--];
    }
    return '\0';
}

// Verifica si el stack está vacío
int isEmpty(CharStack *s) {
    return s->top == -1;
}

// Verifica si el carácter es de apertura
int isOpening(char ch) {
    return ch == '(' || ch == '{' || ch == '[';
}

// Verifica si los símbolos coinciden
int match(char open, char close) {
    return (open == '(' && close == ')') ||
           (open == '{' && close == '}') ||
           (open == '[' && close == ']');
}

// Verifica si la expresión está balanceada
int checkBalanced(const char *expr) {
    CharStack stack;
    initStack(&stack);

    for (int i = 0; expr[i] != '\0'; i++) {
        char ch = expr[i];

        if (isOpening(ch)) {
            push(&stack, ch);
        } else if (ch == ')' || ch == '}' || ch == ']') {
            if (isEmpty(&stack) || !match(pop(&stack), ch)) {
                return 0;
            }
        }
    }

    return isEmpty(&stack);
}

int main() {
    char expression[] = "{7 + (8 * 5) - [(9 - 7) + (4 + 1)]}";

    if (checkBalanced(expression))
        printf("Fórmula balanceada.\n");
    else
        printf("Fórmula no balanceada.\n");

    return 0;
}
