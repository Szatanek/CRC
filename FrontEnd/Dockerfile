FROM node:latest

# Create app directory
RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

# Install app dependencies
COPY package.json /usr/src/app/
RUN npm install

# copy the app source
COPY . /usr/src/app

RUN npm run build

EXPOSE 8080

CMD [ "ng", "serve" ]