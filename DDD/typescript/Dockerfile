FROM node:15

WORKDIR /usr/src/app

RUN npm i typescript --save-dev && \
    npx tsc --init && \
    npm i tslint --save-dev && \
    npx tslint --init && \
    npm i -D jest @types/jest ts-node --save-dev && \
    npm i -D @swc/jest @swc/cli @swc/core && \
    npm i uuid @types/uuid && \ 
    npm install sequelize reflect-metadata sequelize-typescript && \
    npm install sqlite3