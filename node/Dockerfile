FROM node:15

WORKDIR /usr/src/app

# RUN apt-get update 
RUN apt-get install -y wget

ENV DOCKERIZE_VERSION v0.7.0
RUN wget -O - https://github.com/jwilder/dockerize/releases/download/v0.7.0/dockerize-linux-amd64-v0.7.0.tar.gz | tar xzf - -C /usr/local/bin \
    && apt-get autoremove -yqq --purge wget && rm -rf /var/lib/apt/lists/*

EXPOSE 3000

# CMD ["node", "index.js"]