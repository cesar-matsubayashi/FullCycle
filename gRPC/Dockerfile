FROM golang:1.21

WORKDIR /usr/src/app

RUN apt-get update && \ 
    apt-get install protobuf-compiler -y && \
    go install google.golang.org/protobuf/cmd/protoc-gen-go@v1.28 && \
    go install google.golang.org/grpc/cmd/protoc-gen-go-grpc@v1.2 && \
    go mod init github.com/devfullcycle/14-gRPC

RUN go install github.com/ktr0731/evans@latest && \
    go get github.com/mattn/go-sqlite3

# RUN go get github.com/mattn/go-sqlite3

# EXPOSE 8080

# ENTRYPOINT [ "go", "run", "server.go" ]
