FROM mcr.microsoft.com/dotnet/sdk:8.0

 # Install basic development tools
 RUN apt update && apt install -y \
     git \
     procps \
     && rm -rf /var/lib/apt/lists/*

 # Create non-root user
 ARG USERNAME=vscode
 ARG USER_UID=1000
 ARG USER_GID=$USER_UID

 RUN groupadd --gid $USER_GID $USERNAME \
     && useradd --uid $USER_UID --gid $USER_GID -m $USERNAME \
     && apt-get update \
     && apt-get install -y sudo \
     && echo $USERNAME ALL=\(root\) NOPASSWD:ALL > /etc/sudoers.d/$USERNAME \
     && chmod 0440 /etc/sudoers.d/$USERNAME

 # Set the default user
 USER $USERNAME

 # Install global .NET tools
 RUN dotnet tool install -g dotnet-format