FROM golang:1.21

WORKDIR /usr/src/app

RUN go mod init github.com/devfullcycle/13-GraphQL && \
    printf '// +build tools\npackage tools\nimport (_ "github.com/99designs/gqlgen"\n _ "github.com/99designs/gqlgen/graphql/introspection")' | gofmt > tools.go && \
    go mod tidy && \
    go run github.com/99designs/gqlgen init && \
    go mod tidy

# RUN go get github.com/mattn/go-sqlite3

EXPOSE 8080

# ENTRYPOINT [ "go", "run", "server.go" ]
