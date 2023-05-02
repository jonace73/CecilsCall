; ModuleID = 'obj\Release\130\android\compressed_assemblies.arm64-v8a.ll'
source_filename = "obj\Release\130\android\compressed_assemblies.arm64-v8a.ll"
target datalayout = "e-m:e-i8:8:32-i16:16:32-i64:64-i128:128-n32:64-S128"
target triple = "aarch64-unknown-linux-android"


%struct.CompressedAssemblyDescriptor = type {
	i32,; uint32_t uncompressed_file_size
	i8,; bool loaded
	i8*; uint8_t* data
}

%struct.CompressedAssemblies = type {
	i32,; uint32_t count
	%struct.CompressedAssemblyDescriptor*; CompressedAssemblyDescriptor* descriptors
}
@__CompressedAssemblyDescriptor_data_0 = internal global [478208 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_1 = internal global [79872 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_2 = internal global [16384 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_3 = internal global [203776 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_4 = internal global [167424 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_5 = internal global [54784 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_6 = internal global [85504 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_7 = internal global [212992 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_8 = internal global [80792 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_9 = internal global [2370048 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_10 = internal global [121856 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_11 = internal global [90112 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_12 = internal global [690176 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_13 = internal global [5120 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_14 = internal global [10240 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_15 = internal global [100352 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_16 = internal global [5120 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_17 = internal global [46080 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_18 = internal global [5120 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_19 = internal global [35328 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_20 = internal global [14752 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_21 = internal global [394752 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_22 = internal global [747520 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_23 = internal global [26112 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_24 = internal global [168960 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_25 = internal global [230912 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_26 = internal global [39936 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_27 = internal global [7168 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_28 = internal global [513024 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_29 = internal global [56320 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_30 = internal global [65024 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_31 = internal global [1397760 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_32 = internal global [883200 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_33 = internal global [18944 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_34 = internal global [6144 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_35 = internal global [53248 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_36 = internal global [16896 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_37 = internal global [463360 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_38 = internal global [17920 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_39 = internal global [79360 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_40 = internal global [588288 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_41 = internal global [9216 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_42 = internal global [44032 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_43 = internal global [175104 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_44 = internal global [15872 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_45 = internal global [15360 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_46 = internal global [16384 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_47 = internal global [17408 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_48 = internal global [36864 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_49 = internal global [424448 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_50 = internal global [13312 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_51 = internal global [40448 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_52 = internal global [57856 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_53 = internal global [52736 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_54 = internal global [1207296 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_55 = internal global [934912 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_56 = internal global [263040 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_57 = internal global [103424 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_58 = internal global [258048 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_59 = internal global [18072 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_60 = internal global [2172416 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_61 = internal global [483840 x i8] zeroinitializer, align 1


; Compressed assembly data storage
@compressed_assembly_descriptors = internal global [62 x %struct.CompressedAssemblyDescriptor] [
	; 0
	%struct.CompressedAssemblyDescriptor {
		i32 478208, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([478208 x i8], [478208 x i8]* @__CompressedAssemblyDescriptor_data_0, i32 0, i32 0); data
	}, 
	; 1
	%struct.CompressedAssemblyDescriptor {
		i32 79872, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([79872 x i8], [79872 x i8]* @__CompressedAssemblyDescriptor_data_1, i32 0, i32 0); data
	}, 
	; 2
	%struct.CompressedAssemblyDescriptor {
		i32 16384, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([16384 x i8], [16384 x i8]* @__CompressedAssemblyDescriptor_data_2, i32 0, i32 0); data
	}, 
	; 3
	%struct.CompressedAssemblyDescriptor {
		i32 203776, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([203776 x i8], [203776 x i8]* @__CompressedAssemblyDescriptor_data_3, i32 0, i32 0); data
	}, 
	; 4
	%struct.CompressedAssemblyDescriptor {
		i32 167424, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([167424 x i8], [167424 x i8]* @__CompressedAssemblyDescriptor_data_4, i32 0, i32 0); data
	}, 
	; 5
	%struct.CompressedAssemblyDescriptor {
		i32 54784, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([54784 x i8], [54784 x i8]* @__CompressedAssemblyDescriptor_data_5, i32 0, i32 0); data
	}, 
	; 6
	%struct.CompressedAssemblyDescriptor {
		i32 85504, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([85504 x i8], [85504 x i8]* @__CompressedAssemblyDescriptor_data_6, i32 0, i32 0); data
	}, 
	; 7
	%struct.CompressedAssemblyDescriptor {
		i32 212992, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([212992 x i8], [212992 x i8]* @__CompressedAssemblyDescriptor_data_7, i32 0, i32 0); data
	}, 
	; 8
	%struct.CompressedAssemblyDescriptor {
		i32 80792, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([80792 x i8], [80792 x i8]* @__CompressedAssemblyDescriptor_data_8, i32 0, i32 0); data
	}, 
	; 9
	%struct.CompressedAssemblyDescriptor {
		i32 2370048, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([2370048 x i8], [2370048 x i8]* @__CompressedAssemblyDescriptor_data_9, i32 0, i32 0); data
	}, 
	; 10
	%struct.CompressedAssemblyDescriptor {
		i32 121856, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([121856 x i8], [121856 x i8]* @__CompressedAssemblyDescriptor_data_10, i32 0, i32 0); data
	}, 
	; 11
	%struct.CompressedAssemblyDescriptor {
		i32 90112, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([90112 x i8], [90112 x i8]* @__CompressedAssemblyDescriptor_data_11, i32 0, i32 0); data
	}, 
	; 12
	%struct.CompressedAssemblyDescriptor {
		i32 690176, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([690176 x i8], [690176 x i8]* @__CompressedAssemblyDescriptor_data_12, i32 0, i32 0); data
	}, 
	; 13
	%struct.CompressedAssemblyDescriptor {
		i32 5120, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([5120 x i8], [5120 x i8]* @__CompressedAssemblyDescriptor_data_13, i32 0, i32 0); data
	}, 
	; 14
	%struct.CompressedAssemblyDescriptor {
		i32 10240, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([10240 x i8], [10240 x i8]* @__CompressedAssemblyDescriptor_data_14, i32 0, i32 0); data
	}, 
	; 15
	%struct.CompressedAssemblyDescriptor {
		i32 100352, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([100352 x i8], [100352 x i8]* @__CompressedAssemblyDescriptor_data_15, i32 0, i32 0); data
	}, 
	; 16
	%struct.CompressedAssemblyDescriptor {
		i32 5120, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([5120 x i8], [5120 x i8]* @__CompressedAssemblyDescriptor_data_16, i32 0, i32 0); data
	}, 
	; 17
	%struct.CompressedAssemblyDescriptor {
		i32 46080, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([46080 x i8], [46080 x i8]* @__CompressedAssemblyDescriptor_data_17, i32 0, i32 0); data
	}, 
	; 18
	%struct.CompressedAssemblyDescriptor {
		i32 5120, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([5120 x i8], [5120 x i8]* @__CompressedAssemblyDescriptor_data_18, i32 0, i32 0); data
	}, 
	; 19
	%struct.CompressedAssemblyDescriptor {
		i32 35328, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([35328 x i8], [35328 x i8]* @__CompressedAssemblyDescriptor_data_19, i32 0, i32 0); data
	}, 
	; 20
	%struct.CompressedAssemblyDescriptor {
		i32 14752, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([14752 x i8], [14752 x i8]* @__CompressedAssemblyDescriptor_data_20, i32 0, i32 0); data
	}, 
	; 21
	%struct.CompressedAssemblyDescriptor {
		i32 394752, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([394752 x i8], [394752 x i8]* @__CompressedAssemblyDescriptor_data_21, i32 0, i32 0); data
	}, 
	; 22
	%struct.CompressedAssemblyDescriptor {
		i32 747520, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([747520 x i8], [747520 x i8]* @__CompressedAssemblyDescriptor_data_22, i32 0, i32 0); data
	}, 
	; 23
	%struct.CompressedAssemblyDescriptor {
		i32 26112, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([26112 x i8], [26112 x i8]* @__CompressedAssemblyDescriptor_data_23, i32 0, i32 0); data
	}, 
	; 24
	%struct.CompressedAssemblyDescriptor {
		i32 168960, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([168960 x i8], [168960 x i8]* @__CompressedAssemblyDescriptor_data_24, i32 0, i32 0); data
	}, 
	; 25
	%struct.CompressedAssemblyDescriptor {
		i32 230912, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([230912 x i8], [230912 x i8]* @__CompressedAssemblyDescriptor_data_25, i32 0, i32 0); data
	}, 
	; 26
	%struct.CompressedAssemblyDescriptor {
		i32 39936, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([39936 x i8], [39936 x i8]* @__CompressedAssemblyDescriptor_data_26, i32 0, i32 0); data
	}, 
	; 27
	%struct.CompressedAssemblyDescriptor {
		i32 7168, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([7168 x i8], [7168 x i8]* @__CompressedAssemblyDescriptor_data_27, i32 0, i32 0); data
	}, 
	; 28
	%struct.CompressedAssemblyDescriptor {
		i32 513024, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([513024 x i8], [513024 x i8]* @__CompressedAssemblyDescriptor_data_28, i32 0, i32 0); data
	}, 
	; 29
	%struct.CompressedAssemblyDescriptor {
		i32 56320, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([56320 x i8], [56320 x i8]* @__CompressedAssemblyDescriptor_data_29, i32 0, i32 0); data
	}, 
	; 30
	%struct.CompressedAssemblyDescriptor {
		i32 65024, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([65024 x i8], [65024 x i8]* @__CompressedAssemblyDescriptor_data_30, i32 0, i32 0); data
	}, 
	; 31
	%struct.CompressedAssemblyDescriptor {
		i32 1397760, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([1397760 x i8], [1397760 x i8]* @__CompressedAssemblyDescriptor_data_31, i32 0, i32 0); data
	}, 
	; 32
	%struct.CompressedAssemblyDescriptor {
		i32 883200, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([883200 x i8], [883200 x i8]* @__CompressedAssemblyDescriptor_data_32, i32 0, i32 0); data
	}, 
	; 33
	%struct.CompressedAssemblyDescriptor {
		i32 18944, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([18944 x i8], [18944 x i8]* @__CompressedAssemblyDescriptor_data_33, i32 0, i32 0); data
	}, 
	; 34
	%struct.CompressedAssemblyDescriptor {
		i32 6144, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([6144 x i8], [6144 x i8]* @__CompressedAssemblyDescriptor_data_34, i32 0, i32 0); data
	}, 
	; 35
	%struct.CompressedAssemblyDescriptor {
		i32 53248, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([53248 x i8], [53248 x i8]* @__CompressedAssemblyDescriptor_data_35, i32 0, i32 0); data
	}, 
	; 36
	%struct.CompressedAssemblyDescriptor {
		i32 16896, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([16896 x i8], [16896 x i8]* @__CompressedAssemblyDescriptor_data_36, i32 0, i32 0); data
	}, 
	; 37
	%struct.CompressedAssemblyDescriptor {
		i32 463360, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([463360 x i8], [463360 x i8]* @__CompressedAssemblyDescriptor_data_37, i32 0, i32 0); data
	}, 
	; 38
	%struct.CompressedAssemblyDescriptor {
		i32 17920, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([17920 x i8], [17920 x i8]* @__CompressedAssemblyDescriptor_data_38, i32 0, i32 0); data
	}, 
	; 39
	%struct.CompressedAssemblyDescriptor {
		i32 79360, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([79360 x i8], [79360 x i8]* @__CompressedAssemblyDescriptor_data_39, i32 0, i32 0); data
	}, 
	; 40
	%struct.CompressedAssemblyDescriptor {
		i32 588288, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([588288 x i8], [588288 x i8]* @__CompressedAssemblyDescriptor_data_40, i32 0, i32 0); data
	}, 
	; 41
	%struct.CompressedAssemblyDescriptor {
		i32 9216, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([9216 x i8], [9216 x i8]* @__CompressedAssemblyDescriptor_data_41, i32 0, i32 0); data
	}, 
	; 42
	%struct.CompressedAssemblyDescriptor {
		i32 44032, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([44032 x i8], [44032 x i8]* @__CompressedAssemblyDescriptor_data_42, i32 0, i32 0); data
	}, 
	; 43
	%struct.CompressedAssemblyDescriptor {
		i32 175104, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([175104 x i8], [175104 x i8]* @__CompressedAssemblyDescriptor_data_43, i32 0, i32 0); data
	}, 
	; 44
	%struct.CompressedAssemblyDescriptor {
		i32 15872, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([15872 x i8], [15872 x i8]* @__CompressedAssemblyDescriptor_data_44, i32 0, i32 0); data
	}, 
	; 45
	%struct.CompressedAssemblyDescriptor {
		i32 15360, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([15360 x i8], [15360 x i8]* @__CompressedAssemblyDescriptor_data_45, i32 0, i32 0); data
	}, 
	; 46
	%struct.CompressedAssemblyDescriptor {
		i32 16384, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([16384 x i8], [16384 x i8]* @__CompressedAssemblyDescriptor_data_46, i32 0, i32 0); data
	}, 
	; 47
	%struct.CompressedAssemblyDescriptor {
		i32 17408, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([17408 x i8], [17408 x i8]* @__CompressedAssemblyDescriptor_data_47, i32 0, i32 0); data
	}, 
	; 48
	%struct.CompressedAssemblyDescriptor {
		i32 36864, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([36864 x i8], [36864 x i8]* @__CompressedAssemblyDescriptor_data_48, i32 0, i32 0); data
	}, 
	; 49
	%struct.CompressedAssemblyDescriptor {
		i32 424448, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([424448 x i8], [424448 x i8]* @__CompressedAssemblyDescriptor_data_49, i32 0, i32 0); data
	}, 
	; 50
	%struct.CompressedAssemblyDescriptor {
		i32 13312, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([13312 x i8], [13312 x i8]* @__CompressedAssemblyDescriptor_data_50, i32 0, i32 0); data
	}, 
	; 51
	%struct.CompressedAssemblyDescriptor {
		i32 40448, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([40448 x i8], [40448 x i8]* @__CompressedAssemblyDescriptor_data_51, i32 0, i32 0); data
	}, 
	; 52
	%struct.CompressedAssemblyDescriptor {
		i32 57856, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([57856 x i8], [57856 x i8]* @__CompressedAssemblyDescriptor_data_52, i32 0, i32 0); data
	}, 
	; 53
	%struct.CompressedAssemblyDescriptor {
		i32 52736, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([52736 x i8], [52736 x i8]* @__CompressedAssemblyDescriptor_data_53, i32 0, i32 0); data
	}, 
	; 54
	%struct.CompressedAssemblyDescriptor {
		i32 1207296, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([1207296 x i8], [1207296 x i8]* @__CompressedAssemblyDescriptor_data_54, i32 0, i32 0); data
	}, 
	; 55
	%struct.CompressedAssemblyDescriptor {
		i32 934912, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([934912 x i8], [934912 x i8]* @__CompressedAssemblyDescriptor_data_55, i32 0, i32 0); data
	}, 
	; 56
	%struct.CompressedAssemblyDescriptor {
		i32 263040, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([263040 x i8], [263040 x i8]* @__CompressedAssemblyDescriptor_data_56, i32 0, i32 0); data
	}, 
	; 57
	%struct.CompressedAssemblyDescriptor {
		i32 103424, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([103424 x i8], [103424 x i8]* @__CompressedAssemblyDescriptor_data_57, i32 0, i32 0); data
	}, 
	; 58
	%struct.CompressedAssemblyDescriptor {
		i32 258048, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([258048 x i8], [258048 x i8]* @__CompressedAssemblyDescriptor_data_58, i32 0, i32 0); data
	}, 
	; 59
	%struct.CompressedAssemblyDescriptor {
		i32 18072, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([18072 x i8], [18072 x i8]* @__CompressedAssemblyDescriptor_data_59, i32 0, i32 0); data
	}, 
	; 60
	%struct.CompressedAssemblyDescriptor {
		i32 2172416, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([2172416 x i8], [2172416 x i8]* @__CompressedAssemblyDescriptor_data_60, i32 0, i32 0); data
	}, 
	; 61
	%struct.CompressedAssemblyDescriptor {
		i32 483840, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([483840 x i8], [483840 x i8]* @__CompressedAssemblyDescriptor_data_61, i32 0, i32 0); data
	}
], align 8; end of 'compressed_assembly_descriptors' array


; compressed_assemblies
@compressed_assemblies = local_unnamed_addr global %struct.CompressedAssemblies {
	i32 62, ; count
	%struct.CompressedAssemblyDescriptor* getelementptr inbounds ([62 x %struct.CompressedAssemblyDescriptor], [62 x %struct.CompressedAssemblyDescriptor]* @compressed_assembly_descriptors, i32 0, i32 0); descriptors
}, align 8


!llvm.module.flags = !{!0, !1, !2, !3, !4, !5}
!llvm.ident = !{!6}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"branch-target-enforcement", i32 0}
!3 = !{i32 1, !"sign-return-address", i32 0}
!4 = !{i32 1, !"sign-return-address-all", i32 0}
!5 = !{i32 1, !"sign-return-address-with-bkey", i32 0}
!6 = !{!"Xamarin.Android remotes/origin/d17-4 @ 13ba222766e8e41d419327749426023194660864"}
