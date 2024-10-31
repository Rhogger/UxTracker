FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

COPY --from=publish /app/publish/wwwroot . 

RUN chmod 644 nginx.conf
COPY nginx.conf /etc/nginx/nginx.conf

RUN mkdir -p /app/wwwroot/images
RUN chmod -R 755 /app/wwwroot/images

EXPOSE 80

ENTRYPOINT ["nginx", "-g", "daemon off;"]
