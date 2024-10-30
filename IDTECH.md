

# Desarrollo asistido por IA

## Instrucciones

- Abrir el proyecto con Cursor o con VS Code
- El IDE preguntará si desea abrir el proyecto en el devcontainer, responder que sí

Es necesario esperar a que se haga build del container. La primera vez demora varios minutos.

### Aider

- Durante el proceso de inicialización, se mostrará en la Terminal del IDE el nombre de
  usuario del container y una password generada aleatoriamente. Copiar esos datos antes
  de cerrar la terminal.
- Una vez abierto el proyecto, crear un archivo `.env` en el raíz del mismo.
- Completar el archivo `.env` con las keys a LLM que disponga el usuario.
- En el sistema operativo *host*, abrir una terminal nueva y ejecutar:

```shell
ssh -p 2222 -o StrictHostKeyChecking=no -o UserKnownHostsFile=/dev/null -o GlobalKnownHostsFile=/dev/null username@localhost
```

reemplazar `username` por el nombre del usuario obtenido en el primer paso (ej, `node`).

Al ejecutar el comando, el container solicitará una password: ingresar la password copiada.

Una vez dentro del container usando SSH, dirigirse al directorio raíz del proyecto, usualmente
ubicado en `/workspaces/<project_name>` (ej, `/workspaces/luhn`).

Ubicado en el directorio correcto, es posible ejecutar Aider:

```shell
aider
```
