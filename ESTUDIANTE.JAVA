

public class Estudiante {
    // Atributos privados
    private int identificador;
    private String nombre;
    private String apellido;
    private String direccion;
    private String[] telefonos = new String[3]; // Arreglo de 3 teléfonos

    // Constructor con parámetros
    public Estudiante(int identificador, String nombre, String apellido, String direccion) {
        this.identificador = identificador;
        this.nombre = nombre;
        this.apellido = apellido;
        this.direccion = direccion;
    }

    // Método para asignar teléfonos
    public void agregarTelefono(int posicion, String numero) {
        if (posicion >= 0 && posicion < telefonos.length) {
            telefonos[posicion] = numero;
        } else {
            System.out.println("Posición inválida.");
        }
    }

    // Método que imprime la información completa del estudiante
    public void imprimirDatos() {
        System.out.println("ID: " + identificador);
        System.out.println("Nombre completo: " + nombre + " " + apellido);
        System.out.println("Dirección: " + direccion);
        System.out.println("Teléfonos:");
        for (int i = 0; i < telefonos.length; i++) {
            System.out.println("- Teléfono " + (i + 1) + ": " + telefonos[i]);
        }
    }

    // Método main para ejecutar prueba
    public static void main(String[] args) {
        Estudiante estudiante = new Estudiante(2025001, "Carlos", "Mendoza", "Calle Principal #45");
        estudiante.agregarTelefono(0, "0961234567");
        estudiante.agregarTelefono(1, "0981112233");
        estudiante.agregarTelefono(2, "0959988776");
        estudiante.imprimirDatos();
    }
}
