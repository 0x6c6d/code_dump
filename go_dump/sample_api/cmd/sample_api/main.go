package main

import (
	"fmt"
	"net/http"
	"sample_api/internal/routes"
)

func main() {
	router := routes.NewRouter()
	port := 8080
	addr := fmt.Sprintf(":%d", port)
	fmt.Printf("Server listening on http://localhost:%d", port)

	err := http.ListenAndServe(addr, router)
	if err != nil {
		panic(err)
	}
}
