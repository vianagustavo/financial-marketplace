#!/bin/bash

DOTNET_SDK_VERSION=8.0.100
DOTNET_INSTALL_DIR=$HOME/.dotnet
DOTNET_ROOT=$DOTNET_INSTALL_DIR

mkdir -p "$DOTNET_INSTALL_DIR"
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --version $DOTNET_SDK_VERSION --install-dir $DOTNET_INSTALL_DIR

export PATH=$DOTNET_INSTALL_DIR:$PATH

dotnet restore
dotnet publish -c Release -o out
dotnet run --project ./FinancialMarketplace.Api
